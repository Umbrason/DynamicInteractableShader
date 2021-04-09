# Dynamic Interactable Shader
Unity Package containing a base for an interactable shader, with a dynamic interactor count


# How to set up a dynamic material
### Create a new material
Start by creating a material using Shader Graph.\
To be interactable the material must have the following two properties:
* Texture2D '_InteractorPositions'
* Float     '_Interactors' 

### Create a ShaderInteractor
Next create a new GameObject and add a 'ShaderInteractor' component to it.\
Now drag your new material into the material slot of the 'ShaderInteractor' component.\
This component will keep the shader properties updated.

### Create an InteractionAgent
The last thing you need to do is to add the 'InteractorAgent' component to any object, that you want to interact with your material.

# How do I make my material do stuff?
Inside the shader graph you can access the interactor with the strongest influence at any given point using a subgraph called 'NearestPositionFromTexture'.
Just plug in the texture and float variable into the respective sockets and a Vector3 into the position node of the subgraph and it will output the position and strength of the interactor with the strongest influence on the specified point.


