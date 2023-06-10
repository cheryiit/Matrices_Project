# list-stack-queue-priorityQueue_Project
I. YOLCULARI KOLTUKLARA ATAMA
```
```
Bir tur otobüsüne 40 yolcunun y erleştirilmesi planlanmaktadır. Aşağıdaki maddelerde
belirtilen işlemleri yapan kodu yazınız :
```
```
a) Uzaklık Matrisi Oluşturma : 40 adet yolcunun birbirleri ile yakınlık derecelerinin
bilindiği varsayılmaktadır. DM yani Uzaklık matrisi (40x40’lık) içerisine yolcu ikilileri
arasındaki 0 ile 10 arasında rastgele double sayılardan oluşan yakınlık derecelerini
içeren değerleri üretip döndüren metodu yazınız (Tablo 1). 40 elemanlı bir diziye de
yolcu isimlerini (ad soyad) atayınız.
b) Yolcuları Koltuklara Yerleştirme (Yolcuları koltuklara yerleştiren metodu yazınız)
i) Rastgele (random) bir sayı üreterek 1 numaralı koltuğa dizinin o indisindeki yolcuyu
atayınız. 2 numaralı koltuğa, uzaklık matrisinden yararlanarak 1 numaralı koltuğa
yerleştirilen yolcuya en az uzaklıktaki yolcuyu atayınız. 3 ve 4 numaralı koltuklara da,
kalan yolcular içinde kendi sollarındaki koltuğa yerleşen yolcuya en az uzaklıktaki
yolcuları atayınız. İkinci sıraya geçince, 5. koltuk için hemen üstündeki (önündeki) ve
üst çaprazındaki koltuklara (1 ve 2) yerleşen yolculara uzaklıkları toplamı en az olan
yolcuyu atayınız. 6. koltuk için 5, 1, 2 ve 3.; 7. koltuk için 6, 2,3 ve 4.; 8. koltuk için 7,
3 ve 4. koltuklardaki yolculara uzaklıkları toplamı en az olan yolcuları atayınız.
Atamalar, yerleşenler haricindeki yolcular dikkate alınarak yapılacaktır. Bu işlemi, tüm
yolcular koltuklara yerleşene kadar tekrarlayınız.
c) Yolcuları ekrana Tablo 2’deki koltuk yerleşimi düzeninde 4x10 elemanlık bir matris
şeklinde listeleyiniz. Koltuklarda Yolcu numaraları ve Yolcu isimleri olacak şekilde.
d) b maddesindeki hesaba göre koltuklara yerleşen yolcuların toplam uzaklıklarını (sol yan
+ üst (ön) + üst sol çapraz + üst sağ çapraz) yine koltuk düzeninde koltukların üzerinde
olacak şekilde listeleyiniz. İlgili yönde koltuk yoksa o yön için 0 değeri alınacaktır.
e) b maddesine göre yerleşen yolcular arasındaki uzaklıklar toplamını yazdırınız ( tüm
yolcuların uzaklıklar toplamı ). d maddesindeki değerlerin toplamı olan tek değer
istenmektedir.
Tablo 2: Otobüs koltuk düzeni ve numaraları
Tablo 1: Yolcu Çiftleri arasındaki temsili uzaklıklar
```
```
DM[i,j]=DM[j,i]
(i’den j’ye uzaklık ile j’den i’ye uzaklık aynıdır).
```
```
Uzaklık Matrisi (DM)
0 1 2 3 ... 39
0 0 1,2 0,5 4,7 ... 4,
1 1,2 0 3,1 2 ... 4
2 0,5 3,1 0 6,1 ... 1,
3 4,7 2 6,1 0 ... 3,
... ... ... ... ... 0 ...
39 4,9 4 1,9 3,5 ... 0
```
1 2 3 4
5 6 7 8
9 10 11 12
13 14 15 16
17 18 19 20
21 22 23 24
25 26 27 28
29 30 31 32
33 34 35 36
37 38 39 40
