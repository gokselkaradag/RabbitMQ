RabbitMQ Mesaj Kuyruk Sistemi

Bu proje, RabbitMQ kullanarak .NET Core ortamında temel bir mesaj kuyruğu uygulamasını gösterir. Çözüm iki ana bileşenden oluşur: RabbitMQ kuyruğuna mesaj gönderen bir yayıncı (publisher) ve kuyruktan mesajları tüketen bir abone (subscriber).

Proje Yapısı

* RabbitMQPublisher: RabbitMQ kuyruğuna mesaj göndermeyi (publish) yönetir.
* RabbitMQSubscribe: RabbitMQ kuyruğundan mesaj almayı (subscribe) yönetir.
* Shared: Yayıncı ve abone uygulamalarının paylaştığı modeller gibi ortak kaynakları içerir.

Kullanılan Teknolojiler

* RabbitMQ: Dağıtık bileşenler arasında güvenilir mesajlaşmayı sağlayan mesaj aracısı olarak kullanılır.
* .NET Core: Modern uygulama geliştirme için çapraz platform desteği sağlar.


Proje Detayları

* Publisher (Yayıncı): Kuyruğa gönderilen önceden tanımlanmış içeriklerle mesajlar oluşturur. Bu içerikler RabbitMQPublisher/Program.cs dosyasında özelleştirilebilir.
* Subscriber (Abone): Kuyruğu dinler ve gelen mesajları işler, kritik bilgileri log-critical.txt dosyasına kaydeder.
* Ortak Kaynaklar (Shared Resources): Shared dizinindeki Product.cs modeli, her iki uygulama arasında mesaj yapısı olarak kullanılır.

Katkıda Bulunma

Katkıda bulunmak isterseniz, lütfen projeyi fork edin ve pull request gönderin. Her türlü geri bildirim ve katkı projeyi daha da geliştirmemize yardımcı olacaktır.
