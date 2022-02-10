# 5. hafta
4. Hafta başladığım sayfalandırma işleminin url yönlendirme kısmı tamamlandı.

Restful api oluşturun
- Daha önce oluşturduğunuz apilerden 4. haftada oluşturulan api kullanıldı.
- Tek bir endpoint ten Filtreleme, sıralama işlemi sayfalandırılarak yapıldı.
- Demo veri sunan endponte ın memory cache kullanım olanağı sunuldu.
- help endpoint ine api hakkındaki basit bilgileri in memory cache de tutan ve kullanımını sağlayan bir yapı geliştirişdi.
- help endpointinde response cache mekanizmasını kullanıldı
- Servis oluşturulmadan direk injection yapılarak Disributed cache olarak redis e yazma okuma işlemi endt point e eklendi
- Bir de Distributed cache olarak redis e yazan ve okuyan bir cache yönetim servisi yazıldı. Sorgu adedi 100 ve üzeri olursa istenilen zaman aralığında cache yazma ve okuma eklendi. Bknz(IDisributedCacheRedisService)