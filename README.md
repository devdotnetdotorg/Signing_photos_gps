# Signing_photos_gps

**Author:** Anton Serdyukov <anton@devdotnet.org>

**System requirements:** Windows7, .NET Framework 4.5.

**The program writes coordinates (jpg) into images. Introduces gps tags (latitude, longitude, height) of the EXIF ​​standard.
The location of the photo is calculated by the time the photo was created.**

Before you start recording a track, you need to synchronize the time on the camera and Android phone.
If before the photographing the time was not synchronized, then the program needs to set the time correction.
The coordinate file format is Keyhole Markup Language (* .kmz, https://en.wikipedia.org/wiki/Keyhole_Markup_Language).
The program for recording the coordinates of Google My tracks, is removed from Google Play.

To write EXIF ​​tags, use the [System.Windows.Media.Imaging](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging) library

### Screenshots

**Signing_photos_gps**

![Image of htop](https://github.com/devdotnetdotorg/Signing_photos_gps/blob/master/Signing_photos_gps/Screenshots/Signing_photos_gps.png)

**Google My tracks**

![Image of htop](https://github.com/devdotnetdotorg/Signing_photos_gps/blob/master/Signing_photos_gps/Screenshots/My_tracks.jpg)

---
Системные требования: Windows7, .NET Framework 4.5.

Программа записывает в изображения(jpg) координаты. Вносит теги gps(широта, долгота, высота) стандарта EXIF.
Соответствие местоположения фотографии высчитывается по времени создания фотографии.
До начала записи трека требуется синхронизировать время на фотоаппарате и Android-телефоне.
Если до начала фотографирования время не было синхронизировано, то в программе требуется задать коррекцию времени.
Формат файла координат - Keyhole Markup Language (*.kmz, https://en.wikipedia.org/wiki/Keyhole_Markup_Language).
Программа для записи координат Google My tracks(Мои треки).
На данный момент выпилена из Google Play.
Для записи тегов EXIF используется библиотека System.Windows.Media.Imaging.
