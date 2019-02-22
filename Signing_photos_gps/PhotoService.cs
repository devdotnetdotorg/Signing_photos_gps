using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Media.Imaging;
using System.Globalization;
using SharpKml.Dom;
using SharpKml.Base;

namespace Signing_photos_gps
{
    class PhotoService
    {
        public varforbw Params;//Параметры работы с фото, перезапись, добавление времени
        //
        private List<PointGPS> Track;//GPS трек
        public PhotoService()
        {
            Params = new varforbw();
        }
        /// <summary>
        /// Проверка файла с координатами GPS.
        /// Если проверка не проходит, то генерируется исключение
        /// </summary>
        public void ValidateFileGPS()
        {
            if (this.Params.pathFileGPS == null)
            {
                throw new Exception("Отсутствует файл с координатами GPS");
            }
            //
            XmlDocument xDoc = new XmlDocument();
            FileInfo info = new FileInfo(this.Params.pathFileGPS);
            string ext = info.Extension.ToLower();
            XmlDocument Kml = new XmlDocument();
            //Чтение файла, если zip то распаковка
            if (ext == ".kmz")
            {
                MemoryStream data = new MemoryStream();
                using (ZipFile zip = ZipFile.Read(this.Params.pathFileGPS))
                {
                    zip["doc.kml"].Extract(data);
                }
                data.Seek(0, SeekOrigin.Begin);
                xDoc.Load(data);
                Kml.LoadXml(xDoc.InnerXml);
                xDoc = null;
            } else
            {
                StreamReader sr = new StreamReader(this.Params.pathFileGPS, System.Text.Encoding.UTF8);
                Kml.LoadXml(sr.ReadToEnd());
            }
            //Парсинг KML
            parsingKML(Kml);
            //Collect
            //GC.Collect();
        }
        private void parsingKML(XmlDocument xDoc)
        {
            //Выбрка XPath
            //Document/Placemark/Placemark id="tour"/gx:MultiTrack/gx:Track - несколько узлов может быть
            //< when > 2018 - 09 - 14T11: 19:14.406Z </ when >
            //<gx:coord > 20.800204 35.695177 424.20001220703125 </ gx:coord >
            //
            //https://github.com/samcragg/sharpkml
            XmlNodeList nodesMultiTrack = xDoc.GetElementsByTagName("gx:MultiTrack");
            if (nodesMultiTrack.Count < 1)
            {
                //Нет узлов для чтения координат
                throw new Exception("Файл kml не содержит тег gx:MultiTrack для получения координат");
            }
            //Извлечение координат
            var nsManager = new XmlNamespaceManager(new NameTable());
            //register mapping of prefix to namespace uri 
            nsManager.AddNamespace("gx", "http://www.google.com/kml/ext/2.2");
            nsManager.AddNamespace("x", "http://www.opengis.net/kml/2.2");
            XmlNodeList nodesTrack = nodesMultiTrack[0].SelectNodes("gx:Track", nsManager);
            //Список полученых координат
            //Каждая Point организует - Track
            //Tracks список всех Track
            List<PointGPS> track_local = new List<PointGPS>();
            PointGPS pointitem;
            foreach (XmlNode node in nodesTrack)
            {
                XmlNodeList nodeswhen = node.SelectNodes("x:when", nsManager);
                XmlNodeList nodescoord = node.SelectNodes("gx:coord", nsManager);
                //Сопоставление данных
                int i = 0;
                foreach (XmlNode node2 in nodeswhen)
                {
                    string strDate = node2.InnerText;
                    string[] values = nodescoord[i].InnerText.Split(' ');
                    //
                    pointitem = new PointGPS();
                    pointitem.longitude = Convert.ToDouble(values[0].Replace('.', ','));
                    pointitem.latitude = Convert.ToDouble(values[1].Replace('.', ','));
                    pointitem.altitude = Math.Round((Convert.ToDouble(values[2].Replace('.', ','))), 0);
                    pointitem.time = Convert.ToDateTime(strDate);
                    //
                    track_local.Add(pointitem);
                    //
                    i++;
                }
            }
            //Запись треков
            this.Track = track_local;
            //Collect
            xDoc = null;
            nodesMultiTrack = null;
            nodesTrack = null;
            //GC.Collect();
        }
        /// <summary>
        /// Подпись фотографии координатами GPS.
        /// </summary>
        public string WriteGPSinImage_PresentationCore(string PathImage)
        {
            PointGPS out_point;
            return this.WriteGPSinImage_PresentationCore(PathImage, out out_point);
        }
        // <summary>
        /// Подпись фотографии координатами GPS.
        /// </summary>
        public string WriteGPSinImage_PresentationCore(string PathImage, out PointGPS out_point)
        {
            out_point = null;
            //На основе https://habr.com/post/134774/
            //Получение EXIF информации фото
            FileStream Foto = File.Open(PathImage, FileMode.Open, FileAccess.Read); // открыли файл по адресу s для чтения
            BitmapDecoder decoder = JpegBitmapDecoder.Create(Foto, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default); //"распаковали" снимок и создали объект decoder
            BitmapMetadata TmpImgEXIF=(BitmapMetadata)decoder.Frames[0].Metadata.Clone(); //считали и сохранили метаданные
            //Дата создания снимка
            DateTime DateOfShot = Convert.ToDateTime(TmpImgEXIF.DateTaken);
            //Изменение времени, если было задано пользователем
            if (Params.addOrRemoveTime)
            {
                //add time
                DateOfShot = DateOfShot + Params.addTime;
            }
            else
            {
                //Remove time
                DateOfShot = DateOfShot - Params.addTime;
            }
            //Поиск двух ближайших точек, до создания фото и после создания фото
            //Point pointitem;
            //var time = new TimeSpan(3, 0, 0); //---Убрать потом
            //DateOfShot = DateOfShot - time;//---Убрать потом
            PointGPS point_A = null;
            PointGPS point_B = null;
            //Поиск двух точек A и B
            FindPointsAB(DateOfShot, ref point_A, ref point_B);
            //Если точек A и B нет, то невозможно подписать фото
            if ((point_A == null) || (point_B == null))
            {
                return "Для фотографии отсутствуют точки координат, невозможно подписать.";
            }
            //Расчет новой позиции
            PointGPS new_point = PositionCalculation(DateOfShot, point_A, point_B);
            //Изменение EXIF, добавление GPS
            EditEXIF(new_point, ref TmpImgEXIF);
            //Изменение даты фото, если была включена коррекция даты
            if (DateOfShot != Convert.ToDateTime(TmpImgEXIF.DateTaken))
            {
                TmpImgEXIF.DateTaken = DateOfShot.ToString(DateTimeFormatInfo.InvariantInfo);
            }
            //Запись нового файла с тегами
            JpegBitmapEncoder Encoder = new JpegBitmapEncoder();//создали новый энкодер для Jpeg
            Encoder.QualityLevel = 95;//95
            Encoder.Frames.Add(BitmapFrame.Create(decoder.Frames[0], decoder.Frames[0].Thumbnail, TmpImgEXIF, decoder.Frames[0].ColorContexts)); //добавили в энкодер новый кадр(он там всего один) с указанными параметрами
            string NewFileName = new FileInfo(PathImage).Directory + "/" + Path.GetFileNameWithoutExtension(PathImage) + "_.jpg";//имя исходного файла +GeoTag.jpg
            Stream jpegStreamOut = File.Open(NewFileName, FileMode.Create, FileAccess.ReadWrite);//создали новый файл
            Encoder.Save(jpegStreamOut);//сохранили новый файл
            //Clear memory
            Encoder = null;
            decoder = null;
            jpegStreamOut.Flush();
            jpegStreamOut.Close();
            jpegStreamOut.Dispose();
            Foto.Close();
            Foto.Dispose();
            GC.Collect();
            //
            //Делаем замену файла
            //Удаляем старый и переименовываем новый
            File.Delete(PathImage);
            File.Move(NewFileName, PathImage);
            //ok    
            out_point = new_point;
            return "ok";
        }
        /// <summary>
        /// Получение EXIF информации фото
        /// </summary>
        private BitmapMetadata GetImgEXIF(string PathImage, out BitmapDecoder decoder, ref FileStream Foto)
        {
            Foto = File.Open(PathImage, FileMode.Open, FileAccess.Read); // открыли файл по адресу s для чтения
            decoder = JpegBitmapDecoder.Create(Foto, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default); //"распаковали" снимок и создали объект decoder
            //
            return (BitmapMetadata)decoder.Frames[0].Metadata.Clone(); //считали и сохранили метаданные
        }
        /// <summary>
        /// Изменение EXIF информации фото для записи
        /// </summary>
        private void EditEXIF(PointGPS new_point, ref BitmapMetadata TmpImgEXIF)
        {
            //Запись в EXIF
            //широта
            if (new_point.latitude > 0)
            {
                //северная широта
                TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=1}", "N");
            }
            else
            {
                //южная широта
                TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=1}", "S");
            }
            //долгота
            if (new_point.longitude > 0)
            {
                //Восточная долгота
                TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=3}", "E");
            }
            else
            {
                //Западная долгота
                TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=3}", "W");
            }
            //Формат версии
            TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=0}", "2.2.0.0");
            //Высота ->6
            TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=6}", Rational(new_point.altitude));
            //Широта конвертирование в градусы минуты секунды
            double Degree, Minute, Second;
            ConvertToDegreeMinuteSecond(new_point.latitude, out Degree, out Minute, out Second);
            ulong[] t = { Rational(Degree), Rational(Minute), Rational(Second) };
            TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=2}", t);
            //Долгота конвертирование в градусы минуты секунды
            ConvertToDegreeMinuteSecond(new_point.longitude, out Degree, out Minute, out Second);
            ulong[] t2 = { Rational(Degree), Rational(Minute), Rational(Second) };
            TmpImgEXIF.SetQuery("/app1/ifd/gps/{ushort=4}", t2);
        }
        /// <summary>
        /// Поиск двух ближайших точек
        /// </summary>
        private void FindPointsAB(DateTime DateOfShot, ref PointGPS point_A, ref PointGPS point_B)
        {
                //Поиск двух точек внутри Трека
                for (int i = 0; i < this.Track.Count - 1; i++)
                {
                    if ((this.Track[i].time < DateOfShot) && (DateOfShot < this.Track[i + 1].time))
                    {
                        point_A = this.Track[i];
                        point_B = this.Track[i + 1];
                        //
                        return;
                    }
                    //Если время GPS и время фото совпадает
                    if(this.Track[i].time == DateOfShot)
                    {
                        point_A = point_B = this.Track[i];
                        //
                        return;
                    }
                }
                //Проверка последней точки
                if (this.Track[this.Track.Count - 1].time == DateOfShot)
                {
                    point_A = point_B = this.Track[this.Track.Count - 1];
                    //
                    return;
                }
                //
        }
        // <summary>
        /// Расчет точки положения. Исходя из линейной скорости вычисление местоположения
        /// Расчитывается скорость по x и y, затем прибавляется дельта к 
        /// начальной точки A, получаем ответ
        /// </summary>
        private PointGPS PositionCalculation(DateTime DateOfShot, PointGPS point_A, PointGPS point_B)
        { 
            double track_y = point_B.latitude - point_A.latitude;
            double track_x = point_B.longitude - point_A.longitude;
            double track_z = point_B.altitude - point_A.altitude;
            //время движения в секундах
            double tracktime_sec = (point_B.time - point_A.time).TotalSeconds;
            //Стояли на месте
            if(tracktime_sec==0)
            {
                return point_A;
            }
            double speed_y = track_y / tracktime_sec;
            double speed_x = track_x / tracktime_sec;
            double speed_z = track_z / tracktime_sec;
            //Расчет сколько времени двигались от точки A до времени Фото
            double deltatime_sec = (DateOfShot - point_A.time).TotalSeconds;
            //Расчет дельта по x и y
            double delta_y = speed_y * deltatime_sec;
            double delta_x = speed_x * deltatime_sec;
            double delta_z = speed_z * deltatime_sec;
            //Добавление дельты и получение искомой координаты для новой точки
            PointGPS new_point = point_A;
            new_point.latitude = new_point.latitude + delta_y;
            new_point.longitude = new_point.longitude + delta_x;
            new_point.altitude = new_point.altitude + delta_z;
            //
            return new_point;
        }

            /// <summary>
            /// Перевод типа double в Rational
            /// Для Высота на уровнем моря
            /// </summary>
        private ulong Rational(double a)
        {
            uint denom = 1000;
            uint num = (uint)(a * denom);
            ulong tmp;
            tmp = (ulong)denom << 32;
            tmp |= (ulong)num;
            return tmp;
        }
        /// <summary>
        /// Конвертирование в градусы минуты секунды
        /// </summary>
        private void ConvertToDegreeMinuteSecond(double value, out double Degree, out double Minute, out double Second )
        {
            Degree = Math.Floor(value);
            Minute = Math.Floor(((value - Math.Floor(value)) * 60.0));
            Second = (((value - Math.Floor(value)) * 60.0) - Math.Floor(((value - Math.Floor(value)) * 60.0))) * 60;
        }
        /// <summary>
        /// Создание KML файла по фоотографиям
        /// </summary>
        public string CreateKML(List<KeyValuePair<string, PointGPS>> listPointsWithPhoto)
        {
            Console.WriteLine("Creating a point at 37.42052549 latitude and -122.0816695 longitude.\n");

            // This will be used for the placemark
            Point point = new Point();
            point.Coordinate = new Vector(27.42052549, -15.0816695);

            Placemark placemark = new Placemark();
            placemark.Name = "Cool Statue";
            placemark.Geometry = point;

            // This is the root element of the file
            Kml kml = new Kml();
            kml.Feature = placemark;

            Serializer serializer = new Serializer();
            serializer.Serialize(kml);
            Console.WriteLine(serializer.Xml);

            Console.WriteLine("\nReading Xml...");

            Parser parser = new Parser();
            parser.ParseString(serializer.Xml, true);

            kml = (Kml)parser.Root;
            placemark = (Placemark)kml.Feature;
            point = (Point)placemark.Geometry;

            Console.WriteLine("Latitude:{0} Longitude:{1}", point.Coordinate.Latitude, point.Coordinate.Longitude);

            //
            return "ok";
        }
    }
    //
    struct varforbw
    {
        public string pathImages;
        public string pathFileGPS;
        public bool overwriteGPSInfo;
        public bool addOrRemoveTime;
        public System.TimeSpan addTime;
    }

    public class PointGPS
    {
        public double latitude;
        public double longitude;
        public double altitude;
        public DateTime time;
    }
}
