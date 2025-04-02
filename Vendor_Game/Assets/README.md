# Workshop UI (02/04/2025)

--- 
> Max Janssen, Giannini Pruijn, Haroon Muhammad, Nusayba El Mourabet en Kyan Kersten.
--- 
Het doel van deze workshop is om de basis van Unity UI te leren. 
We gaan een game maken waarin de speler items op kan pakken, kan ruilen met een villager en items op kan slaan in een kast. De logica van deze game is al gemaakt, maar de UI ontbreekt nog.

## 1. Project Openen 
Ga naar de [demo repo](https://github.com/MaxJanssen2002/UI_Research_Prototypes/tree/main/Vendor_Game) op GitHub en clone de ``workshop`` branch. Open vervolgens het project in Unity. 
De scene die geopend wordt is de Main scene, deze is op het moment nog leeg. 

1. Sleep de world prefab in de scene. 
2. Sleep de player prefab in de scene.

De scene is nu een stuk minder leeg en de speler kan rondlopen. Het is echter vrij lastig om te zien waar je muis zit en weten we niet hoeveel emeralds de speler heeft. 

## Stap 1: Speler UI 

### 1.1 Crosshair
1. Begin met het aanmaken van een nieuwe canvas. ``(GameObject -> UI -> Canvas)``. Dit is de basis van je UI, hier kunnen we nieuwe elementen aan toevoegen.
2. Een voorbeeld van zo'n element is een ``Raw Image``. Voeg deze toe aan de canvas.
3. In de Inspector, zet de positie op ``Pos X en Pos Y op 0``. Dit zorgt ervoor dat de crosshair in het midden van het scherm staat. Zet de ``Width en Height`` wat lager (bijvoorbeeld naar 50).
4. Klik op de cirkel naast 'Texture' en selecteer de afbeelding ```"Crosshair"```. 
![img.png](img.png)

### 1.2 Emerald saldo
1. Voeg een ``Raw Image`` en ``Text`` toe aan de canvas. Let op, deze tekst moet TextMeshPro zijn. Hierbij wordt er gevraagd of je TMP Essentials wilt importeren, klik op ``Import TMP Essentials``.
2. Verander de texture van de afbeelding naar ```"Emerald"```.
3. Selecteer het canvas in de Hierarchy en druk op de F toets. 
4. Verplaats de afbeelding naar de linkerbovenhoek van het canvas. Verplaats de tekst naar rechts van de afbeelding. 
5. Onder rect transform in de Inspector zie je de Anchor Presets. Zet deze naar ```Top``` en ```Left```. Doe dit voor beide elementen. Dit zorgt ervoor dat de elementen op de juiste plek blijven staan, ongeacht de grootte van het scherm.
6. Ga terug naar de Editor en sleep de door jou gemaakte tekst naar de nieuwe variabele. 
![img_1.png](img_1.png)

Als je nu op play drukt, zie je dat de crosshair en het emerald saldo zichtbaar zijn!

De canvas schaalt op het moment nog niet mee met de grootte van het scherm. Dit kan je aanpassen door in de inspector de ```Canvis Scaler -> UI Scale Mode``` naar ```Scale with Screen Size``` te zetten.

## Stap 2: Items toevoegen aan een Inventory
## Stap 3: Items ruilen met een Villager 
## Stap 4: Items opslaan in een kast
In de winkel is een lege kast te zien. Deze heeft 12 slots waarin de speler items kan opslaan. Op het moment is het niet zichtbaar hoeveel items er in de kast zitten en welke prijs deze items hebben. 

