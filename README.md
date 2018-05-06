# VR_Project3
### Proposal Phase
## Descriptive Title:
  - Improved Water Elevation
## General Discription: 
  - We will fully adapt our project with the Oculus. We will autogenerate the terrain or add more cities. We will add the yearly saturated thickness data. We will add more prefabs to decorate the terrain. In addition, we will create new buttons to teleport to different wells. We will also add more facts about each city. In addition, we will also add music that people can hear while they are playing because that is an important aspect. The reason why VR is beneficial is that VR allows the user to get a firsthand perspective of the landscape of the city they choose, which fosters an immersive experience. In addition, users can feel a more real experience than looking at a 2D representation by using VR.
## Hardware platform we will use:
  - PC, Oculus rift, Google Cardboard
## Software we will use:
  - Unity
## Teammates:
  - Wenhao Ge, Lino, Kevon
  -+   - Made a drop-down memu in the main menu and the drop-down menu contains five choices
+   - Made a slider
+   - Wrote script to show the scale change of the slider when user slides the slider
+   - Wrote script to let slider call different functions when the value in the slider changes
+   - Wrote script to read county,latitude,longitude,water elevation,saturated thickness data and all other data from a .csv file
+   - Wrote script to display the information read from the .csv file when the mouse hover over the each well
+   - Made a panel in the right top corner in unity. When the mouse hovers over each well, well-related information can show up in the panel
+   - Chose the terrain and exported the terrain from the height map 
+   - Created the rain effect and dynamic cloud effect by using particle systems
+   - Created a water fountain by using particle system
+   - Created four buttons called 'Spring",'Summer','Fall,'Winter'. When 'Fall' is clicked, the scene starts raining. When other three buttons are clicked, rain stops 
+   - Changed the underground water layer thickness by clicking the previous four buttons
+   - Wrote scripts to let buttons control the rain audio
+   - Found out the monthly precipitation data in Lubbock and recorded the data in a csv document
+   - Read data from the csv document and calculate the average precipitation data for each season.
+   - Modified the water layer and set the water thickness by using the average precipitation data for each season
+   - Decorated the terrain by using trees, grass and houses
+   - Wrote script to let the water fountain appear inside of the wells when users click play button
  - Wenhao's role: He will use prefabs to decorate the terrain and add some animations to GameObjects. 
  - Kevon’s role: He will create several buttons to teleport to different wells and do other things.
  - Lino's role: He will do the terrain autogeneration
## Links to any datasets or libraries:
- Texas Water Development Board (https://www.twdb.texas.gov/groundwater/data/gwdbrpt.asp)
- iDataVisualizationLab at Texas Tech University (https://github.com/iDataVisualizationLab/SaturatedThickness)
- Terrain.party (www.terrain.party)
- Unity Asset Store
  
