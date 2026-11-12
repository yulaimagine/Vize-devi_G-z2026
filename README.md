# VizeÖdevi_Güz2026

Oyunumuz, küre şeklindeki oyuncumuzun yeşil toplardan aldığı +10'ar puanlarla toplam en az 50 puana ulaşıp bitiş platformuna zıplayarak leveli bitmesinden oluşuyor. Kırmızı topu alırsa oyuncumuz -5 puan alıyor. Oyuncumuz zeminden düşerse oyun tekrardan başlıyor. Yeteri kadar puan toplayamaz ise seviyeyi bitiremeyeceği uyarısını alıyor.

Player	Sphere, tag: Player, Rigidbody (Freeze Rotation X-Y-Z açık, Linear Damping 0.5), Sphere Collider, PlayerController scripti
Ground	Plane/zemin, tag: Ground
Engeller	Cube'lerden oluşan engeller, tag: Ground (üzerlerinde de zıplanabilmesi için)
Yeşil toplar	Sphere, Is Trigger açık, tag: PositiveCoin
Kırmızı toplar	Sphere, Is Trigger açık, tag: NegativeCoin
Bitiş alanı	Cube, Is Trigger açık, FinishZone scripti
Canvas / Skor yazısı	TextMeshPro yazısı, Player üzerindeki Score Text alanına bağlı
Main Camera	Kamera takip scripti, Player alanına Player bağlı

PlayerController.cs	Rigidbody ve AddForce ile hareket, isGrounded kontrolü ve zıplama, OnTriggerEnter ile nesne toplama, puan hesabı, skor yazısının güncellenmesi, haritadan düşünce sahneyi yeniden başlatma
FinishZone.cs	Bitiş alanı. Oyuncu puanı hedef puana ulaştıysa SceneManager ile sahneyi yeniden yükler, ulaşmadıysa oyunu sonlandırmaz
CameraFollow.cs	Kameranın oyuncuyu sabit bir mesafeden takip etmesi

Değişkenler: int (score, targetScore), float (speed, jumpForce, horizontal, vertical), bool (isGrounded, jumpRequested)
if / else koşullu ifadeleri
Metotlar: MovePlayer(), Jump(), CheckGround(), UpdateScoreUI(), RestartGame()
Awake(), Start(), Update(), FixedUpdate(), LateUpdate()
GetComponent<Rigidbody>() ve GetComponent<PlayerController>()
Rigidbody ve AddForce()
OnCollisionEnter / Stay / Exit ile isGrounded kontrolü
OnTriggerEnter() ve CompareTag()
Destroy()
SceneManager.LoadScene()
TextMeshPro ile skor gösterimi

Update() içinde Input.GetAxis("Horizontal") ve Input.GetAxis("Vertical") ile tuş girdisi okunur.
FixedUpdate() içinde bu değerlerden new Vector3(horizontal, 0f, vertical) hareket yönü oluşturulur.
rb.AddForce(movement * speed) ile oyuncuya kuvvet uygulanır. Böylece küre doğrudan taşınmaz, fiziksel olarak hızlanır.
speed değişkeni Inspector'dan değiştirilebilir. Çok kayma olursa Rigidbody üzerindeki Linear Damping artırılır.

Update() içinde Space tuşuna basıldı mı ve isGrounded true mu kontrol edilir. İkisi de doğruysa jumpRequested = true olur.
FixedUpdate() içinde jumpRequested true ise rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse) ile anlık yukarı kuvvet uygulanır.
Zıplamadan hemen sonra isGrounded = false yapılır. Böylece oyuncu havadayken tekrar zıplayamaz.
OnCollisionEnter ve OnCollisionStay içinde oyuncu Ground tag'li bir nesneye üstten temas ederse (temas yüzeyinin normal.y değeri 0.5'ten büyükse) isGrounded = true olur.
OnCollisionExit içinde Ground nesnesinden ayrılınca isGrounded = false olur.

1 InvalidOperationException: You are trying to read Input using the UnityEngine.Input class... ve oyuncu hareket etmiyordu.	Unity 6 projesinde yeni Input System aktifti, kodda ise eski Input.GetAxis kullanılıyordu.	Project Settings → Player → Active Input Handling ayarı Input Manager (Old) (veya Both) yapıldı.
2	Tag: PositiveCoin is not defined ve Tag: NegativeCoin is not defined	Bu tag'ler projede henüz oluşturulmamıştı.	Tags and Layers bölümünden PositiveCoin ve NegativeCoin tag'leri eklendi, toplara atandı. Tag adları büyük-küçük harfe duyarlı olduğu için birebir aynı yazıldı.
3	NullReferenceException ... PlayerController.UpdateScoreUI()	scoreText alanı Inspector'da boştu, yazı nesnesi bağlanmamıştı.	UI'daki TextMeshPro yazısı Player üzerindeki Score Text alanına sürüklenerek bağlandı.
4	NullReferenceException ... CameraFollow.LateUpdate()	Kamera scriptindeki Player alanı boştu, takip edilecek nesne atanmamıştı.	Hierarchy'deki Player nesnesi bu alana sürüklendi.
5	W/S ve A/D tuşları ters çalışıyordu	Hareket dünya eksenlerine göre yapılıyor, kamera ise Player'a ters taraftan bakıyordu.	Kameranın konumu (offset değeri) Player'ın arkasına (−Z tarafına) alındı, böylece W ileri, D sağa gitti.
6	Skor yazısı ekranda görünmüyordu	Skor yazısı olarak hazır şablondan gelen MessageText nesnesi bağlıydı. Nesne Canvas'ın altında değildi / ayarları (konum, renk, boyut) uygun değildi	Yazı nesnesi Canvas'ın altına alındı, konumu sol üst köşeye ayarlandı, font boyutu ve rengi düzenlendi.

Havada zıplama: Oyuncu havadayken Space'e basıldığında tekrar zıplamadığı test edildi (isGrounded false iken zıplama isteği oluşmuyor).
Yetersiz puanla bitiş: Puan 50'nin altındayken bitiş alanına girildiğinde oyunun sonlanmadığı test edildi.
Yeterli puanla bitiş: 50 ve üzeri puanla bitiş alanına girildiğinde sahnenin yeniden başladığı, puanın sıfırlandığı ve topların geri geldiği test edildi.
Kırmızı top: Kırmızı top toplandığında puanın 5 azaldığı test edildi.
Haritadan düşme: Oyuncu zeminden düştüğünde oyunun yeniden başladığı test edildi.