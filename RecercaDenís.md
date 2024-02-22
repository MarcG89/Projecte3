<h1>CREAR INSTAL·LADOR O ARXIU .EXE A TRAVÉS DE D’UNA APP WPF</h1>

<h2>Requeriments</h2>

![Descarregar Microsoft Visual Studio Installer Projects](/Captures/descarregarExtensio.png)
<p>Abans de començar el procés, cal que primer et descarreguis l’extensió anomenada <strong>Microsoft Visual Studio Installer Projects</strong>.</p>

![Instal·lar Microsoft Visual Studio Installer Projects](/Captures/instalarExtensio.png)
<p>Seguidament, has de tancar Visual Studio perquè s’instal·li l’extensió i s’apliquin els canvis. T’hauria d’aparèixer aquesta finestra on has de fer clic al botó <i>Modify</i>.

<h2>Procés</h2>

![Afegir projecte Storage](/Captures/afegirStorageSetup.png)
<p>Quan ja tinguis instal·lada l’extensió, crea un segon projecte dins la mateixa solució que més endavant ens servirà per fer el Setup.</p>

<p>Un cop hagis creat el projecte Setup, podràs veure aquestes tres carpetes:</p>

<ul>
  <li><strong>Application’s folder</strong> (on es guarden els  fitxers .exe)</li>
  <li><strong>User’s Desktop</strong></li>
  <li><strong>User’s Programs Menu</strong></li>
</ul>

![Project Output](/Captures/projectOutput.png)
<p>Tot seguit, copiem el fitxer els fitxers <i>.exe</i> i <i>.dll</i> necessaris. Per fer-ho, has d’entrar dins de la <i>Application’s folder</i>, fer clic dret en ella, escollir l’opció <i>Add</i> i, dins d’aquesta, triar l’opció <i>Project Output</i> o <i>Resultados del proyecto…</i></p>

<p>Se t’obrirà un conjunt d’opcions, on has d’escollir <i>Primary output</i> o <i>Resultado principal</i>.</p>

![Fitxer Exe Projecte Setup](/Captures/fitxerExe.png)
<p>Amb això ja tens el fitxer .exe afegit.</p>

<h2>Afegir shortcut</h2>

![ShortCut afegida](/Captures/shortCutAfegida.png)
<p>Per afegir un shortcut (en aquest cas es diu <i>AppSetupFile</i>), has de fer clic dret al fitxer <i>.exe</i> i després escollir la primera opció (que et dirà <i>Create Shortcut</i>). Pots canviar-li el nom sempre que vulguis.</p>

<h3>Afegir i assignar icona al Shortcut</h3>

![Icona ShortCut afegida](/Captures/iconaShortCutAfegida.png)

<p>En cas de que vulguis afegir una icona al Shortcut, has de fer clic drent, anar a l'opció <i>Add</i> i, dins d'aquesta, triar l'opció <i>File</i> per després buscar el fitxer que vols afegir. La que utilitzarem en aquest projecte és la que veus sota aquest paràgraf. Tingues en compte que només podràs utilitzar fitxers de tipus .ico</p>

![Icona ShortCut a utilitzar](/Captures/iconaShortCut.ico)

![PropertiesWindow de ShortCut](/Captures/shortCutPropertiesWindow.png)

<p>Ja només ens falta vincular la icona amb el Shortcut, ho aconsegueixes fent clic dret sobre el ShortCut accedint a la <i>Properties Window</i> i finalment afegint la icona dins de l'atribut <i>Icon</i>.</p>

