# What is this package about?
This Unity Package contains a base for an **interactable shader**, with a **dynamic interactor count**!\
![InteractableShaderScreenshot](https://user-images.githubusercontent.com/45980080/114247004-62feb000-9994-11eb-9d45-ed66c504d2ce.PNG)

Using only **two scripts** you can provide a shadergraph with **updating positions of any number of gameObjects**.\
This package was created for the use with **shadergraph** in the **UPR or HDRP**.\
Each interactor provides the material with its position and radius of influence, allowing for dynamic materials to interact with objects in the scene.

# How to set up a dynamic material
### Create a new material
Start by creating a material using Shader Graph.\
To be interactable the material must have the following two properties:
* Texture2D '_InteractorPositions'
* Float     '_Interactors' 

![Properties](https://user-images.githubusercontent.com/45980080/114247747-3186e400-9996-11eb-90c5-cc0b9695885e.PNG)

### Create a ShaderInteractor
Next create a new GameObject and add a 'ShaderInteractor' component to it.\
Now drag your new material into the material slot of the 'ShaderInteractor' component.\
This component will keep the shader properties updated.\
![ShaderInteractor](https://user-images.githubusercontent.com/45980080/114247043-76aa1680-9994-11eb-8c47-3e4b7f9d4ab8.PNG)

### Create an InteractionAgent
The last thing you need to do is to add the 'InteractorAgent' component to any object, that you want to interact with your material.\
You also need to drag your new material into the material slot of the 'InteractionAgent' component.\
The 'InteractionSize' property determines the 'strength' or 'radius' of the influence the 'InteractionAgent' has.
![Interaction Agent](https://user-images.githubusercontent.com/45980080/114247037-74e05300-9994-11eb-8a5d-5fb3bd8c74cb.PNG)

# How do I make my material do stuff?
Inside the shader graph you can access the interactor with the strongest influence at any given point using a subgraph called 'NearestPositionFromTexture'.\
Just plug in the texture and float variable into the respective sockets and a Vector3 into the position node of the subgraph and it will output the position and strength of the interactor with the strongest influence on the specified point.\
![NearestPositionSubgraph](https://user-images.githubusercontent.com/45980080/114247641-e1a81d00-9995-11eb-8ec3-4c9a52257bc9.PNG)


