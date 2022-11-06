# User Documentation

All credits for Pokemon sprites go to  Nintendo and The Pokemon  Company.

This tool can be used to generate landscapes, side on levels and textures along a 2D plane. 
Included in the project are two pre-made tile sets, one of which is a simple set of lines that is more demonstrative of the texture generation ability while the other is demonstrative of the terrain generation ability.

## Scripts
The main part of this is the Grid builder script which needs to be attached to an empty gameobject.
In this script you will find a few options:
- auto restart, which will make the grid automatically reset when a failed state is encountered
- Use weighting, which will toggle if tile weighting is accounted for
- propagation distance, which determines how far out information is passed from a tile when it is collapsed (note this is limited from 0 to 2 as anything beyond two at the current level causes a huge performance impact)
- Delay which determines how long each tile will take before solving. If this is set to 0 the whole grid will be solved before being rendered.
- Width and Height, the dimensions of the grid
- Use complex edges, this is a partial implementation of a new feature and should be left as on until this document is updated
- Set, this is the set of tiles you would like to use, you can create a new set scriptableobject via the context menu -> Create -> Module -> new tileSet, this set should contain all of the tiles you would like the algorithm to use
- the modules list should be left empty as it will fill itself out with the provided set at runtime
- Tile is the base tile prefab, if you intend to use this as is then this should not be changed. However you can swap this out when modifying the system for your own use as will be discussed below.

You will also find three other scripts related to the algorithm in this project: Cell, Module, and ModuleSet.

Cell has no options you will need to change although you are able to view a cells properties at runtime by viewing it in the inspector.

ModuleSet is not strictly necessary and is provided as a usability feature. It allows for modules to provided to the grid in sets rather than indivisually in the inspector, making it easy to swap out whole tilesets quickly.

Module is a scriptable object that holds all of the information that a cell is filled with when collapsed. If you would like to make a new tileset you will need to make a new module for each tile with unique edges.

In editor modules have a few propertys you will need to know:
Weighting determines the likelyhood of a tile being picked from the possible tiles, note this is not a percentage and due to nature of how constraint solving and weighting interracts, it is hard to describe what "appropriate" values are and it is best to experiment,

The default sprite is the one that will be used if none are added to the pool. Set this if a tile has unique edges that it does not share with any other.

The sprite pool is used if you have multiple tiles that share the same edges. For example in the pokemon tileset, all flowers and grass share the same edges and therefore are lumped into one "grass" module. Doing this creates a notable performance incerease.

Sprite pool weighting is used to set the weighting of sprites within a modules sprite pool.

!!!IMPORTANT!!! 
Unfortunately Unity does not render dictionaries in the inspector and so in order to do this I have been forced to use two lists. As a result you will need to make sure the sprite pool and sprite pool weighting lists are the same length.

Edges are used to determine how tiles are allowed to connect. Edges are counterclockwise from the bottom meaning the first edge represents the bottom most edge where the last represents the left most edge.

Edge codes can be as long or short as desired and an edge can connect to another with the same code. For example ABA can connect to another ABA but not a ABB. To understand this better it is best to observe the provided tile modules to see how the codes relate to the sprites.

## Using this outside of the project
Note that when using this outside of the context of the provided project the set scale function is not necessary although may prove useful as it only make sure that the grid fits into the same space regardless of grid dimensions.

It is possible to Modify this to work in a 3D project by simply changing the coordinates from vector2s to vector3s in the code, namely the GridBuilder script. 
You would also need to make some changes like changing the collider type on the cell and so forth.

It is also possible to replace the sprites with GameObjects and meshes. Strictly speeking a cell is just a gameObject with the script on it, the 2dbox colliders are just to allow for clicking on a cell to collapse it.
in this if you changed all of the sprit variables in the Cell and Module to GameObject variables and made modules store prefabs this can be somewhat easily converted into something that can place 3D meshes along a 2D plane.
However much more drastic changes would be required to add an actual third dimension to this algorithm. 
