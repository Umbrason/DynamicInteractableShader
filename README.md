# Dynamic Interactable Shader
Unity Package containing a base for an interactable shader, with a dynamic interactor count


# How to get started
### Create a new material
Start by creating a material using Shader Graph.\
To be interactable the material must have the following two properties:
* Texture2D '_InteractorPositions'
* Float     '_Interactors' 

### Create a ShaderInteractor
Next create an empty gameObject and add a 'ShaderInteractor' component to it.\
Now drag your new material into the material slot of the 'ShaderInteractor' component.\
This component will keep the shader properties updated.

### Create an InteractionAgent
The last thing you need to do is to add the 'InteractorAgent' component to any object, that you want to interact with your material.
