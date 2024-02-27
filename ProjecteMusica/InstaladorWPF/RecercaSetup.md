<h1>CREAR INSTAL·LADOR O ARXIU .EXE A TRAVÉS DE D’UNA APP WPF</h1>

<h2>Requeriments</h2>

![Descarregar Microsoft Visual Studio Installer Projects](Captures/descarregarExtensio.png)
<p>Abans de començar el procés, cal que primer et descarreguis l’extensió anomenada <strong>Microsoft Visual Studio Installer Projects</strong>.</p>

![Instal·lar Microsoft Visual Studio Installer Projects](Captures/instalarExtensio.png)
<p>Seguidament, has de tancar Visual Studio perquè s’instal·li l’extensió i s’apliquin els canvis. T’hauria d’aparèixer aquesta finestra on has de fer clic al botó <i>Modify</i>.

<h2>Procés</h2>

![Afegir projecte Setup](Captures/afegirSetupProject.png)
<p>Quan ja tinguis instal·lada l’extensió, crea un segon projecte dins la mateixa solució que més endavant ens servirà per fer el Setup.</p>

<p>Un cop hagis creat el projecte Setup, podràs veure aquestes tres carpetes:</p>

![Carpetes SetupProject](Captures/afegirSetupProject.png)
<ul>
  <li><strong>Application’s folder</strong> (on guardarem la referència del projecte i la icona de l'instal·lador)</li>
  <li><strong>User’s Desktop</strong></li>
  <li><strong>User’s Programs Menu</strong></li>
</ul>

![Project Output](Captures/projectOutput.png)
<p>Tot seguit, afegirem la referència del projecte MusicalyAdminApp. Per fer-ho, has d’entrar dins de la <i>Application’s folder</i>, fer clic dret en ella, escollir l’opció <i>Add</i> i, dins d’aquesta, triar l’opció <i>Project Output</i> o <i>Resultados del proyecto…</i></p>

<p>Se t’obrirà un conjunt d’opcions, on has d’escollir l'opció <i>Publicar elementos</i>.</p>

![Fitxer Exe Projecte Setup](Captures/projectOutputAfegit.png)
<p>Amb això ja tens la referència del projecte afegida.</p>

<h2>Afegir shortcut</h2>

![ShortCut afegida](Captures/shortCutAfegida.png)
<p>Per afegir un shortcut (en aquest cas es diu <i>ReproductorInstaller</i>), has de fer clic dret a la referència del projecte i després escollir la primera opció (que et dirà <i>Create Shortcut</i>). Pots canviar-li el nom sempre que vulguis.</p>

<h3>Afegir i assignar icona al Shortcut</h3>

![Icona ShortCut afegida](Captures/iconaShortCutAfegida.png)

<p>En cas de que vulguis afegir una icona al Shortcut, has de fer clic drent, anar a l'opció <i>Add</i> i, dins d'aquesta, triar l'opció <i>File</i> per després buscar el fitxer que vols afegir. La que utilitzarem en aquest projecte és la que veus sota aquest paràgraf. Tingues en compte que només podràs utilitzar fitxers de tipus .ico</p>

![Icona ShortCut a utilitzar](Captures/iconaShortCut.ico)

![PropertiesWindow de ShortCut](Captures/shortCutPropertiesWindow.png)

<p>Ja només ens falta vincular la icona amb el Shortcut, ho aconsegueixes fent clic dret sobre el ShortCut accedint a la <i>Properties Window</i> i finalment afegint la icona dins de l'atribut <i>Icon</i>.</p>

![Guardar ShortCut al User's Desktop](Captures/shortCutDinsDeUsersDesktop.png)
![Guardar ShortCut al User's Programs Menu](Captures/shortCutDinsDeUsersProgramsMenu.png)

<p>Quan ja tinguis la icona afegida al ShortCut, mou aquest dins de la carpeta <i>User's desktop</i> i crear una altre ShortCut amb la mateixa icona per també afegir-la dins de la carpeta <i>User's Programs Menu</i> o la carpeta del menú del programa.</p>

![Propietats de SetupProject](Captures/propietatsSetupProject.png)
<p>Si vols, pots configurar les propietats del projecte del SetupProject com ara el nom de l'autor o una descripció</p>

<h2>Prerequisits i administració de configuració</h2>

![Accedir a prerequisits projecte](Captures/accedirPrerequisitsProjecte.png)
<p>Per configurar els prerequisits del projecte, hem d'accedir a les propietats del projecte i fer clic sobre el botó <i>Prerequisites</i>. Per defecte tindràs activat el <i>Microsoft .NET Framework</i>.</p>

![Administrador Configuracio](Captures/administradorConfiguracio.png)
<p>Finalment, només ens faltarà anar a l'Administrador de configuració.  Aquest és l'apartat on es defineixen les opcions del compilador i els valors de compilació que s'utilitzen quan es compila el projecte. Les configuracions que tens per defecte són <i>Debug</i> i <i>Release</i>. La configuració <i>Debug</i> admet la depuració d'una aplicació, mentre que la <i>Release</i> compila una versió de l'aplicació que es pot implementar. En aquest cas, apliquem la configuració <i>Release</i> a tots els projectes menys el que utilitzarem per fer el Setup.</p>

![Fitxer .exe creat](Captures/fitxerExeCreat.png)
<p>Finalment, només et quedarà compilar el projecte perquè se't generi el fitxer .exe i a punt per executar-lo.</p>

<h2>Procés instal·lació</h2>

![Escollir directori fitxers aplicació](Captures/seleccionarCarpetaInstalador.png)
![Instal·lació completa](Captures/instalacioCompleta.png)
<p>El procés d'instal·lació serà simple i bàsic, només et farà falta indicar a quin directori voldràs que es guardin tots els fitxers necessaris perquè funcioni el projecte i esperar a que tot s'instal·li correctament.</p>

![Aplicació afegida a l'Escriptori](Captures/appDesktop.png)
<p>Quan hagis acabat tot el procés d'instal·lació, ja se t'haurà generat l'aplicació a l'escriptori i podràs provar-la.</p>

![Finestra ChooseDockerAndApi](Captures/chooseDockerAndApi.png)
<p>En cas de que no tinguis Docker instal·lat, veuràs la finestra <i>ChooseDockerAndApi</i></p>

![Finestra MainWindow](Captures/mainWindow.png)
<p>En cas contrari, veuràs la <i>MainWindow</i>.</p>