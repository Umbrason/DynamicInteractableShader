# Dynamic Interactable Shader
Unity Package containing a base for an interactable shader, with a dynamic interactor count


# How to get started
Start by creating a dynamic material using Shader Graph.
The material has to have the following two properties:
-Texture2D _InteractorPositions
-Float _Interactors
Place one instance of the 'Interaction Agent' Script on any object, that should interact with your Shader, per material you want to interact with. You also need one instance of the 'ShaderInteractor' per interactive material
