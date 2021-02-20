# Alkfejl II. kötelező program I. (2020)
 Olympic history I.

## Feladat

A feladat megadott bemeneti fájlok feldolgozása, majd LINQ segítségével végzett műveletek után kimenet generálása .csv fájlokba.

A program célja LINQ, és fájl írás/olvasás műveletek megismerése .NET-ben.

### Eredeti kiírása

Adottak a következo CSV fájlok: athlete_events, athletes. A CSV-k
olimpiai versenyzoket (athletes) és olimpiai versenyszámokat tartalmaznak
(athlete_events) (nem az összes eddigi adatot). A feladatok a következok:
1. Készítsd el a CSV-kben található adattípusoknak megfelelo osztályokat
külön-külön fájlokban (propertyk használatával).
2. Olvasd be a fájlokat, tárold el egy tetszoleges kollekcióban a tartalmukat.
3. Készíts egy harmadik kollekciót, ami a versenyzok id-ja alapján összefésüli
az adatokat és a következo tulajdonságokat tartalmazza: versenyzo id-ja,
neve, csapata, sportág amiben versenyzett.
4. Számold meg, hogy hány versenyzo szerzett érmet.
5. Versenyzonként számold meg, hogy hány érmet szereztek.
6. Ezek közül keresd meg a legtöbb érmet szerzett versenyzot.
7. Listázd ki azokat a versenyzoket, akik érmet szereztek és úszóként indultak.
8. Keresd meg a legmagasabb norvég versenyzot.
9. Listázd ki azokat a versenyzoket, akik 30-nál idosebbek voltak, amikor
indultak az olimpián, az életkoruk szerint növekvo sorrendbe rendezd oket.
10. Minden feladat megoldását írd ki egy taskX CSV fájlba, ahol X a megfelelo
feladat sorszáma. Pl. a 3. feladat megoldása a task3.csv fájlba kell
kerüljön.

## Megvalósítás

### Bemenet
 Indításhoz az athletes_fixed.csv-t illetve az athlete_events.csv-t az Input mappába kell rakni. 
 A .csproj-ban meg van adva hogy automatikusan bemásolja, de ha valami baj lenne vele a \bin\Debug\netcoreapp3.1\Input mappában nyitja meg a fájlokat.
 
### Kimenet
 Futás végén kiírja a console hogy melyik mappába helyezte a kimeneteket, de Dotnet run esetén a projekt gyökér mappájából az \APHCJW_Olympic_History_1\Output-ba helyezi a kimeneteket
 VS indítás esetén pedig gyökér mappából indulva a \APHCJW_Olympic_History_1\APHCJW_Olympic_History_1\bin\APHCJW_Olympic_History_1\Output-ba fogja helyezni
 
 Dotnetes futás esetén nem akartam a projekten kívülre helyezni semmit, az útvonalakkal pedig nem akartam túlkomplikálni azért néz ki így.
 
### Project felépítés
 Az 1. 2. 3. feladathoz szükséges osztályok a /Core/Tables alatt vannak, a többi feladat, és a program törzse az OlympicCore.cs-ben.
 A projekt a CsvHelper NuGet package-et használja 
