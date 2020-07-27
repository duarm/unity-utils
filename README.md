# unity-utils

Organizing some of my useful standalone scripts/tools.

## PIVOT HELPER
Place inside an Editor Folder.
Window Path: Otker>Pivots

### Propagate First Pivot
How to use: Manually set the pivot of the first Sprite of a sprite sheet, select the spritesheet and call Otker>Pivots>Propagate First Pivot. The first pivot will propagate to every sprite on that spritesheet. You can select multiple spritesheets at a time, each one with its own first sprite pivot.

### Round>Upper Upper/Upper Bottom/Bottom Upper/Bottom Bottom
How to use: Select a spritesheet and call Otker>Pivots>Round>* to round the pivot to the ceil(Upper) or to the floor(Bottom) in the x and y axis respectively. E.g. Pivot is x:0.5 y:0.5 which in that particular sprite, translates to x:6.5 y:5 in Pixel Unit, the sprite will be misaligned because its pivot is in between two pixels in the X axis, calling the Upper/\*, the pivot will be set to x:7 y:5, calling the Bottom/\*, the pivot will be rounded to x:6 y:5. You can select multiple spritesheets at a time.
