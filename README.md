# Pawel_Kielanowski_187724
PLATFORMER

Opis gry: 
Gra polega na zebraniu jak największej ilości punktów w ograniczonym czasie. Punkty zbieramy poprzez zabijanie wrogów. Z wrogów którzy uciekają, wypadają skrzynie które dają nam upgrade. 
Dostępne upgrady:
-Dodatkowych 10 sek czasu.
-Szybsze strzelanie na 5 sek.
-Doładowanie do maksimum obecnego życia.
Sterowanie:
A- poruszanie się w lewo.
B-poruszanie się w prawo.
SpaceBar-podskok
Left Ctrl - strzelanie.


Zaimplementowane rzeczy niezbędne do zaliczenia Programowania z wykorzystaniem technologii CUDA i OpenCL:
- Wybrałem obliczenia równoległe na wątkach. Na wątku pobocznym jest oparty deszcz, oraz jego per pixel collision z graczem, oraz przeciwnikami. Z mapą kolizja występuję jako zwykłe kwadraty. Za syncrhonizację wątku pobocznego z głównym posłużyłem się eventami. Mianowicie na początku każdej pętli Update jest zdejmowana blokada z wątku pobocznego, po czym na końcu wątek główny czeka ew. na wątek poboczny gdyby ten dłużej pracował i ponownie stawia na nim blokadę. Sytuacja wygląda podobnie w pętli Draw.

Kolejną rzeczą obowiązkową (zadaną przez Pana pracę domową), czym jest obrót collidera wraz z obrotem sprite`a. Została ona zaimplementowana tylko i wyłącznie w kolizjach pomiędzy deszcze - przeciwnikami, oraz deszczem playerem. W przypadku obrotu sprite, również obraca się jego collider czyli tj color, ponieważ kolizja jest per pixel collision. Klasa DebugSprite służyła do tego bym mógł to sprawdzić.

Z powodu iż jest to pierwsza przeze mnie tworzona gra, posiłkowałem się różnymi tutorialami, oraz rozwiązaniami z internetu, np. pomysł na mapę, lub obrót pixeli wraz z spritem.

Planuję również 'pobawić' się w dalszy rozwój gry, ponieważ generalnie tematyka mnie zaciekawiła, a zwłascza cała matematyka w grach, która jeszcze bywa dla mnie wyjątkowo uciążliwa, także nawet w trakcie trwania tej sesji, mimo po otagowaniu ostatniego commita jako FINAL proszę się nie zdizwić jak jeszcze będę coś dodawał. Jeśli uważa Pan że rzeczy dodane po tj wyżej FINAL COMMIT`cie nie powinny być ocenione, oczywiście to rozumiem, po prostu nie chcę kończyć, a jeśli jest szansa że dodam coś jeszcze co dodatkowo wpłynie pozytwnie na moją ocenę, to tym bardziej.
Paweł Kielanowski
