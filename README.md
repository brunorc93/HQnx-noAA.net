# No AntiAliasing HQ2nx upscaling filter

This was coded with C# initially for Unity3D and then adapted for C#.net due to low dependency on Unity. 
It can be compiled with .net sdk 5 using ``dotnet build`` or ``dotnet run`` in the my-app folder throught the command line.

It builds a command line application that scales a preexisting image and saves the scaled image either as one bigger image or as several chunks with the same size as the original image.
  
---------------------------------------------------------------------------
This is one module of a series used on Unity3D to generate island meshes. Other modules adapted for C#.net can be seen in the following links:
* [Island Shape](https://github.com/brunorc93/islandShapeGen.net)  
* [Biome Growth](https://github.com/brunorc93/BiomeGrowth.net)  
* [Noise - previous](https://github.com/brunorc93/noise)  
* [empty repo](empty repo)  

> (more links will be added as soon as the modules are ported onto C#.net).  

The full Unity Project can be followed [here](https://github.com/brunorc93/procgen)  

---------------------------------------------------------------------------

This module scales the image by doubling its size a given number of times. The maximum number of times it may be at once has been set to 4 resulting in an image 256 times (16x16) larger than the initial one. 

It is based off of the original [HQx filter](https://en.wikipedia.org/wiki/Hqx) and the code used as reference can be found [here](https://github.com/Tamschi/hqxSharp). The main difference is that the resulting image uses only the colors available in the original image with the purpose of smoothing its borders between colors.

The results of scaling an image up using this method can be seen in the immages available in the following table:

| [Base image](examples/base.png) | saved as a            | saved as                                  |
| ----------- | ----------------------------------------- | ----------------------------------------- |
| Scaled 2x   | [single image](examples/single-x2.png)    | [multiple images](examples/multi-x2.png)  |
| Scaled 4x   | [single image](examples/single-x4.png)    | [multiple images](examples/multi-x4.png)  |
| Scaled 8x   | [single image](examples/single-x8.png)    |                                           |
| Scaled 16x  | [single image](examples/single-x16.png)   | [multiple images](examples/multi-x16.png)  |

Another example in the following images:

* Base image (full image + zoomed to a specific area)
<div style="display: inline-block">
    <img style="float: left;" src="examples/base.png?raw=true" width="400" height="400" alt="base">
    <img style="float: left;" src="examples/base-zoom.png?raw=true" width="400" height="400" alt="zoomed base">
</div>

* Image scaled 16x (full image + zoomed to the same area)
<div style="display: inline-block">
    <img style="float: left;" src="examples/single-x16.png?raw=true" width="400" height="400" alt="scaled 16x">
    <img style="float: left;" src="examples/single-x16-zoom.png?raw=true" width="400" height="400" alt="zoomed scaled 16x">
</div>  