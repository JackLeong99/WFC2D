# comp3180-project

All credits for Pokemon sprites go to  Nintendo and The Pokemon  Company.

Included is a rough timeline below. Fill this in from week to week to show your progress throughout semester

## week 1 - Selecting a Project

My topic is: terrain generation with wave function collapse

## week 2 - Reading

## week 3 - Reading
Great general explaination videos of the concepts behind wave function collapse  
[WFC for procedural generation](https://www.youtube.com/watch?v=20KHNA9jTsE)  
[Sudoku allegory](https://www.youtube.com/watch?v=2SuvO4Gi7uY)  
[Oskar Stalberg | EPC2018](https://www.youtube.com/watch?v=0bcZb-SsnrA) (yes i know mitch posted this but id already watched it anyway)  

things that I have found but have yet to take an extensive look at (Update: have looked at as of W4)  
[Oskar Stalberg "Wave" demo](https://oskarstalberg.com/game/wave/wave.html)   
[Demo based on generating tileset from an input image](https://bolddunkley.itch.io/wave-function-collapse)  

[Short article + demo](https://marian42.de/article/wfc/)  
[Longer article on WFC theory](https://robertheaton.com/2018/12/17/wavefunction-collapse-algorithm/)  

[Original WFC github](https://github.com/mxgmn/WaveFunctionCollapse)  
[WFC branch](https://github.com/shawnridgeway/wfc)  
[WFC contraints branch](https://github.com/zfedoran/go-wfc?ref=golangexample.com)  

/* sources on other terrain generation algorthisms */

[PMD doc 1](https://docs.google.com/document/d/1HuJIEOtTYCtSHK6R-sp4LC2gk1RDL_mfoFL6Qn_wdkE/edit)  
[PMD doc 2](https://docs.google.com/document/d/1UfiFz4xAPtGd-1X2JNE0Jy2z-BLkze1PE4Fo9u-QeYo/edit)  

## week 4 - Reading

What I did this since last week: Since last week was my first real week and I spent most of it just finding starting points for research,  this week I spent the time to better read through these sources and understand them fully as well as which ones I can still learn from and which I have gotten all I can.

Better defining my project goals:  
  - create a simple wfc implementation with 5 tiles: one blank and four "T" shaped, one in each possible rotation.  
  - expanding this to a more complex tile set to generate a 2D "terrain" or "landscape" similar to Oskar stalbergs "wave", with the main difference functionally being   that this algorithm should handle asymetrical tiles.  
  - going back to the first tileset/algorithm but adding random rotation such that the number of tiles is reduced to two, one blank and one "T" where the "T" tile can   be randomly rotated to functionally fill the role of 4 tiles.  
  - create an equivelent 3D implementation of the second algorithm, one that can handle asymetrical, predefined tiles without rotation.  
  - create a 3D implementation that reduces the number of premade tiles by handling random rotation.  
  - Write accompanying technical document.  

[Implementation video on tile model](https://www.youtube.com/watch?v=rI_y2GAlQFM)  
[Processing article on WFC with additional sources on implementation](https://discourse.processing.org/t/wave-collapse-function-algorithm-in-processing/12983)  

I think the model that I am interested in for the sake of this project is known as the "tile model"  

tile model implies the necessity of date structures to hold the information of tiles which can be made with abstract code.  

[Procjam tutorial containing info on both models](https://www.procjam.com/tutorials/wfc/)  
the overlapping model can be linkened to the mathematical principal: "markov chain"  



## week 5 - Reading & Research Report

This week I started on my research report and did some additional reading on new sources I plan to adress in said report.  
In doing this I finalised and expanded upon my learning goals/outputs as well as settled on my 6 sources. It is possible I will replace the last source (Robert Heaton article) with my first prototype if I am able to produce it in time. 

Sources:  
  - [Original GitHub repo](https://github.com/mxgmn/WaveFunctionCollapse)  
  - [Thread about the algorithm on Processing](https://discourse.processing.org/t/wave-collapse-function-algorithm-in-processing/12983)  
  - [A high quality youtube video including both a conceptual breakdown of the algorithm as well as going through implementation](https://www.youtube.com/watch?v=rI_y2GAlQFM)  
  - [Acedemic paper on wave function collapse](https://adamsmith.as/papers/wfc_is_constraint_solving_in_the_wild.pdf)  
  - [Artcile on wave function collapse](https://www.gridbugs.org/wave-function-collapse/)  
  - [Second article on wave function collapse](https://robertheaton.com/2018/12/17/wavefunction-collapse-algorithm/)  

Outputs:  
I will be breaking this down into two sections, learning outcomes and tangible outputs, the latter of which will be further broken down into subgoals.   

Learning outcomes:  
  - Gain a better understanding of how the wave function collapse algorithm works, including at least a general understanding of both models.  
  - Gain a better understanding of the tile model of the wave function collapse algorithm specifically.  
  - Learn how to go about turning these learned concepts into working code.  
  - Learn about adding constraints to a wave function collapse algorithm.  

Tangible outputs:
  - Primary goals:  
    I will have 4 primary goals, three are to produce prototypes of tile model wave function collapse algorithms where each one builds on top of knowledge from the         previous. The fourth goal is to produce accompanying documentation that both explains my learning process as well as the algorithms that I have produced.  
      - Accompanying documentation:  
        The documentation will be written alongside the learning process and show how I went about learning, as well as a section at the end explaining the working of any     code produced as part of the other goals.  
      - 2D Prototype I:  
        The first prototype will be a simple implementation of the tile model that contains 5 tiles. One blank (or plus shaped, both serve the same function in this           context) and four “T” shaped tiles, one for each possible orientation.  
      - 2D Prototype II:  
        Modify the existing algorithm to be able to handle asymmetrical tiles with an expanded tileset. This algorithm should be able to generate side-on 2D terrain. The       effective end goal of this prototype is to recreate a program similar to Oskar Stalberg’s [“Wave”](https://oskarstalberg.com/game/wave/wave.html) 
      - 2D Prototype III:  
        This prototype will use knowledge and some existing code from the previous two prototypes and will be able to generate 2D terrain from a top down perspective,         think of a much simpler version of games such as dwarf fortress. The difficulty here will be adding constraints such that the algorithm will create logical terrain     with continuous groupings of things such as rivers, oceans and forests. An example of this type of generation is the generation shown at 6:46 in                       [this](https://youtu.be/20KHNA9jTsE?t=396) video versus what is shown around [8:40](https://youtu.be/20KHNA9jTsE?t=520).  
  - Stretch goals:  
    These stretch goals serve as ways for me to further my learning and abilities if I am able to complete my primary goals earlier than anticipated.  
      - 3D Prototype I:  
        This prototype will effectively be a three dimensional implementation of the second 2D algorithm, able to generate 3D terrain given a predefined set of                 asymmetrical tiles.  
      - 2D Prototype IV:  
        This will be a middle step to assist with the next prototype and will be a modified version of the very first 2D algorithm. The modification will allow for tile       rotation such that the only two tiles should be the blank tile and a “T” shaped one. This means that the “T” shaped tile can be rotated and thus function as 4         tiles.  
      - 3D Prototype II:  
        This will be a modification of the first 3D algorithm but will allow for tiles to be rotated around the vertical axis (as in they can be rotated horizontally).  

## week 6 - Presentation week 1

## week 7 - Presentation week 2

## Mid-semster break 1

## Mid-semster break 2

## week 8 - Prototyping

https://github.com/atalantus/WFC-Unity-Example

## week 9 - Prototyping

## week 10 - Prototyping

## week 11 - Evaluation

## week 12 - Evaluation
Things to adress for overall project:
    Stackoverflow errors when working with very large grids and no delay. From my understanding so far this is because I am using a generic sort function, see below:
    https://stackoverflow.com/questions/64584290/getting-stack-overflow-exception-while-sorting-an-array-with-15000-elements
    https://stackoverflow.com/questions/20415044/stackoverflowexception-when-perform-quicksort-with-ordered-list
    https://stackoverflow.com/questions/33452614/quicksort-algorithm-cormen-gives-stackoverflow

    Backtracking is really nessecary for more complex tilesets to operate at larger grid sizes.
    As the more either of these factors increase, (tileset complexity/grid size) the more likely it is to run into an unsolvable state forcing a redo.
    Adding backtracking is a common inclusion in these algorithms as it is generally the best/only realistic solution in adressing this problem.
    This is because backtracking allows the program to avoid a completely new attempt each time one of these situations is encountered by simply trying another solution to the previous tile.
    However this is easier said than implemented as you need to also check if the previous tile actually had more than one possible solution.
    If not you need to go further back until one that had multiple options is reached and then continue from there.
    This requires an ordered list of collapsed tiles so that in case of failure it can read through these to find an appropriate place to start from.
    Implementing this in my algorithm is very doable however.
        Update: Upon further research this a much bigger task than I thought and could be out of scope for this project, 
        especially since it only increases the "effective" success rate of the algorithm (which is already good), but does not further my progress towards my prototype 3.

Things to adress for achieving prototype 3:
    Without having looked at any examples of constraints beyond presetting certain tiles, my first instinct in achieving more logical generation is to add some kind of weighting to the random choices.
    To further explain, at the moment my algorithm is searching for the lowest entropy value and solving that.
    When it tries to collapse a tile, it randomly choses between all of the remaining possible options that haven't yet been removed by adjacently solved tiles.
    By adding weighting to this choice, I should be able to encourage certain types of tiles and thus certain shapes.
    For context I am thinking about this in the context of the pokemon tileset. By weighting grass higher than ledges, I would be able to discourage lots of random and illogical looking edges.


    Update: I am testing a rudimenatry weighting system and this is my favourite line of code I've written so far:
        activeModule = entropy == 1 ? availableModules[0] : grid.useWeighting ? RandomWithWeight(availableModules) : availableModules[Random.Range(0, availableModules.Count)];


    My current weighting seems to produce something approaching the desired behaviour but is flawed in a few ways.
    My Biggest gripe with my current generation is that I cant discourage certain things from generating together while still allowing them to be valid choices.
    Again using this Pokemon tileset for an example. I dont like how at the moment, complete ledges can generate adjacent to eachother. 
    If a horizonatal ledge generates, ideally I would like to prevent another from generating directly above or below it.


    One idea I have to further encourage the correct behavious and avoid the problem above, is something im calling "Likeness".
    Each tile can have a category assigned to it and if a neighbouring tile falls into the same category then it gains an increased weighting.
    This however will require a more sophisticaed weighting system compared to what I have as of writing this.
    Update: Having further improved the weighting system and having experimented with a rough implementation of this idea,
    I belive the concept of a seconday adjacency condition could solve one of the problems I have with my algorithm although this exact approach will likely be far too much work for little reward.


    Another Idea is multi code edges allowing for more sophisticated adjacency.


    Another idea: fruthering propogation. At the moment, when a cell collapses, it only propogates information to the nearest cell. 
    By furthinging this such that all cells that should be updated are, before the next collapse, we can reduce the chance of failure greatly.
        Update: This worked... to an extent. It does mitigate failure to a noticable degree however it does so at a significant performance cost.

    Another major optimization thats easy to implement is combining tiles with the same edges by turning a tiles sprite into a list of sprites and randomly picking from that.
        Update: I have now added this to great success. Not only has it significantly improved performance but it has also allowed for the addition of "sub weighting". 
        For context: How it is currently set up is that any sprite that has the same set of edges (ie all grass and flower tiles), are stored in one tile. 
        When this tile is placed it will then randomly pick from one of sprites. Sub weighting is simply refering to the addition of weighting values to this randomness.
        This is MUCH faster than having one tile per sprite.

TODO before report:

    (In order of highest to lowest priority)    

    - Close out any thoughts above [Incomplete]
    - Add remaining tiles to pokemon tileset [Complete]
    - Add overlay generation (optional) [Dropped]
    - Add controllable character (optional) [Dropped]
    - Improve sorting method (optional) [Dropped*]
    - (New) Added a UI 
    
    *Dropped due to discovering that the issue was more likely to do with the number of game objects rather than the sorting method, although it still stands that it could be improved for speed.

## week 13 - Finalising the project & report
