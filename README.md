# RaylibDoomFire
The famous Doom fire implemented with Raylib in C#. See a preview bellow:

<br><br/>
<p align="center">
  <img src="assets/DoomFire.GIF" />
</p>
<br><br/>

## Build and Run

The system dependencies will be fetch with the `dotnet restore` command, Visit the [Raylib-cs Repo](https://github.com/ChrisDill/Raylib-cs),
to check if your OS and cpu architecture is supported.

### Build and run on release mode:

```
dotnet restore
dotnet build -c Release -o out
./out/DoomFireRaylib
```
