



## SfmlDrawPerformance:

### Versions
Polling based [34d34004](https://github.com/oskar-anderson/DotnetPerformanceTest/tree/34d34004)  
Event based [de3d15cd](https://github.com/oskar-anderson/DotnetPerformanceTest/tree/de3d15cd)

### Results
Platvorm;Kaadrite sagedus sekundites (FPS)  
Windows 10 desktop  kasutades pollimise põhist sisendit ja vektorpõhist fonti;64.6  
Linux Mint 20.1 desktop kasutades pollimise põhist sisendit ja vektorpõhist fonti;6.0  
Windows 10 desktop kasutades sündmuse põhist sisendit ja vektorpõhist fonti;265.3  
Linux Mint 20.1 desktop kasutades sündmuse põhist sisendit ja vektorpõhist fonti;250.0  
Windows 10 desktop kasutades sündmuse põhist sisendit ja rasterfonti;463.3  
Linux Mint 20.1 desktop kasutades sündmuse põhist sisendit ja rasterfonti;333.3  


### Getting started
```
git clone https://github.com/oskar-anderson/DotnetPerformanceTest
cd ./DotnetPerformanceTest/SfmlDrawPerformance
git reset --hard commit_hash_here__example_34d34004
dotnet run
```

__Keymapping__:  
* W - Show Image  
* Q - Randomize image  
* E - raster font randomize  


### Other
Publishing `SfmlDrawPerformance`:
```
dotnet publish -r linux-x64 --self-contained=true
dotnet publish -r win-x64 --self-contained=true
```

Linux client side dependency:
```
sudo apt install libcsfml-dev
```

