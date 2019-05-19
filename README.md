# snake-game
other snake name, now in csharp ;)

# Porque?
_no hay porque_

La idea es refactorizar el código y pasar de estructurado a OP.

Trate de representar las clases necesarias para la ejecución del juego, y aislar las responsabilidades.

Cada vez que un método quedaba grande, lo separo en métodos mas pequeños que sean mas explicativos (_y mas testeables_, si hubiese hecho los tests).

Use algunas interfaces, solo algunas donde se veían útiles, al no usar IoC no quise gastar mucho tiempo en eso.

Use algunos patrones, tal como Facade o Singleton. Algunos quedaron en _TODO_ y otros a medio camino (hay un casi obsever), seguramente hayan algunos _antipatrones_

Intente apegarme a SOLID, pero algo se me habra pasado.

Creo que cumple con DRY.



## WHERE IS MY EXE!!???
Easy, easy, this project run on dotnet core 2.1, so, you need specify the runtime to _publish_ then and get the exe.

If you don't have time for read, run this:

```shell
dotnet publish -r win10-x64
```
an look on `bin\Debug\netcoreapp2.2\win10-x64`

![mind-blow](https://media.giphy.com/media/xT0xeJpnrWC4XWblEk/giphy-downsized.gif)

of course, if you're run on win10.

### Not on Win10?
Great! see the runtimes on: [dotnet RIDs](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog#using-rids)
