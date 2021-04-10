# What is this package about?
This Unity Package contains a base for an **interactable shader**, with a **dynamic interactor count**!\
This means, that once you set up your material you can **add any number of interactors** and even **create or destroy them during runtime**.\
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
Now drag the new material into the material slot of the 'ShaderInteractor' component.\
This component will keep the shader properties updated.\
![ShaderInteractor](https://user-images.githubusercontent.com/45980080/114247043-76aa1680-9994-11eb-8c47-3e4b7f9d4ab8.PNG)

### Create an InteractionAgent
The last thing you need to do is to add the 'InteractorAgent' component to any object, that you want to interact with your material.\
You also need to drag the new material into the material slot of the 'InteractionAgent' component.\
The 'InteractionSize' property can be used to control the the 'strength' or 'radius' of the influence the 'InteractionAgent' has.
![Interaction Agent](https://user-images.githubusercontent.com/45980080/114247037-74e05300-9994-11eb-8a5d-5fb3bd8c74cb.PNG)

# How do I make my material do stuff?
Inside the Shadergraph you can access the interactor with the strongest influence at any given point using a Subgraph called 'NearestPositionFromTexture'.\
Plug in the Texture2D and Float properties into their respective sockets and a Vector3 into the position node of the Subgraph and it will output the position and 'InteractionSize' of the InteractorAgent with the strongest influence on any given position.\
![NearestPositionSubgraph](https://user-images.githubusercontent.com/45980080/114247641-e1a81d00-9995-11eb-8ec3-4c9a52257bc9.PNG)

### Calculate the influence of the nearest InteractionAgent
Using this information you can now compute the influence of the nearest 'InteractorAgent' and the specified point.\
The 'InteractionSize' property of an InteractionAgent can be accessed via the 4th component of the InteractionAgent position.\
Using this property you can normalize the values be in the range of 0-1.\
![normalizedDistance](https://user-images.githubusercontent.com/45980080/114248711-a5c28700-9998-11eb-8e93-5d93bc017e67.PNG)


### Calculate the direction towards the nearest InteractionAgent
You can compute the normalized direction towards the nearest 'InteractorAgent'\
![NormalizedDirection](https://user-images.githubusercontent.com/45980080/114248707-a529f080-9998-11eb-877c-94a91f081dc1.PNG)

### Use the influence and direction to displace geometry
Using this influence and direction its possible to move geometry away from the nearest 'InteractionAgent' by multiplying the direction vector with the influence and adding this to the position of each vertex.
![VertexDisplacement](https://user-images.githubusercontent.com/45980080/114249975-744bba80-999c-11eb-82a1-8e6e17696aaa.PNG)

# Example: Interactive Grass Shader
Below is a GIF of a **grass shader reacting** to the **position of multiple InteractionAgents** represented as drones.\
The 'InteractionSize' property of the InteractionAgent is scaled by the magnitude of the scale of each drone, resulting in a wider effect radius for the large drone.\
They move around in real time and their position is updated in the shader respectively.\
![InteractiveGrassShader](https://user-images.githubusercontent.com/45980080/114252098-e2e04680-99a3-11eb-9c89-0bc20f8932b8.gif)\

The effects in the GIF were achieved using the **vector displacement** method mentioned above but multiplied with the **sine of the distance to the most influencial  InteractionAgent** and with its phase animated over time.
The illusion of the grass bending in the wind is just the result of two gradient noise samples added to the vertex displacement and offset over time.
