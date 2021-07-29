# RandomPersonTextureBehaviour
Script for a random human or android texture (like RandomSpriteBehaviour).

## Set up
1. Place the "RandomPersonTextureBehaviour.cs" script in the mod folder.   
![изображение](https://user-images.githubusercontent.com/65243242/127415189-adc9b258-89ec-4891-882d-5b0df93890c1.png)
2. Add the path to the script in mod.json.   
![изображение](https://user-images.githubusercontent.com/65243242/127415255-463b9e84-6392-4c8a-989b-2fc1a0668d10.png)


## How to use
1. Add the "RandomPersonTextureBehaviour" component to person.
```cs
var rptb = Instance.AddComponent<RandomPersonTextureBehaviour>();
```
2. Add 3 sprites (name_skin.png & name_flesh.png & name_bone.png).  
![изображение](https://user-images.githubusercontent.com/65243242/127415432-ca87e0ac-9e87-4b72-9ed8-d24706516066.png)


3. Add a new skin.
```cs
rptb.AddSkin("cool skin"); // you can also add a texture scale and override path in method parameters (Ex: "cool skin", 1, "textures/").
rptb.AddSkin("also cool skin");
```
4. Enjoy!   
![изображение](https://user-images.githubusercontent.com/65243242/127415682-0d5a08cc-0729-48f0-bd6d-08b4de7402d7.png)
