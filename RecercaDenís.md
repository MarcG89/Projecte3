<h1>CREAR INSTAL·LADOR O ARXIU .EXE A TRAVÉS DE D’UNA APP WPF</h1>

<h2>Requeriments</h2>
![Descarregar Microsoft Visual Studio Installer Projects](/Captures/descarregarExtensio.png)
<p>Abans de començar el procés, caldrà que primer et descarreguis l’extensió anomenada <strong>Microsoft Visual Studio Installer Projects</strong>.</p>

![Instal·lar Microsoft Visual Studio Installer Projects](/Captures/instalarExtensio.png)
<p>Després caldrà que tanquis Visual Studio perquè s’instal·li l’extensió i s’apliquin els canvis. T’hauria d’aparèixer aquesta finestra on hauràs de fer clic al botó <i>Modify</i>.

<h2>Procés</h2>
![Afegir projecte Storage](/Captures/afegirStorageSetup.png)
<p>Quan ja tinguis instal·lada l’extensió, caldrà que creis un segon projecte dins la mateixa solució que servirà per fer el Setup.</p>

<p>El projecte Setup tindrà tres carpetes:</p>

<ul>
  <li><strong>Application’s folder</strong> (on es guarden els  fitxers .exe)</li>
  <li><strong>User’s Desktop</strong></li>
  <li><strong>User’s Programs Menu</strong></li>
</ul>

![Project Output](/Captures/projectOutput.png)
<p>Tot seguit, caldrà que copiem el fitxer els fitxers <i>.exe</i> i <i>.dll</i> necessaris. Per fer-ho, hauràs d’entrar dins de la <i>Application’s folder</i> i a dins fer clic dret, escollir l’opció <i>Add</i> i, dins d’aquesta, triar l’opció <i>Project Output</i> o <i>Resultados del proyecto…</i></p>

<p>Se t’obrirà un conjunt d’opcions, on hauràs d’escollir <i>Primary output</i> o <i>Resultado principal</i>.</p>

![Fitxer Exe Projecte Setup](/Captures/fitxerExe.png)
<p>Amb això ja tindràs el fitxer .exe afegit.</p>

<h2>Afegir shortcut</h2>
<p>Per afegir un shortcut (en aquest cas es diu <i>AppSetupFile</i>), hauràs de fer clic dret al fitxer <i>.exe</i> i després escollir la primera opció (que et dirà <i>Create Shortcut</i>). Podràs canviar-li el nom sempre que vulguis.</p>