
using Raylib_cs;
using System.Numerics;
using System.Security;

//nueva ventana y camara
Raylib.InitWindow(1280 ,720, "mVentana");

Camera2D camera = new Camera2D();
camera.Offset = new Vector2(1280/2,720/2);
camera.Zoom = 1;

// nueva escena y GameObjects player, enemigos y projectiles. 
Scene scene = new Scene();
GameObject player = scene.CreateGameObject(new Vector2(100,100), new Vector2(50,50), Color.Red, 200, 10);
player.isPlayer=true;
GameObject.Player = player;

GameObject[] enemy = new GameObject[20];

for(int i=0; i<20; i++)
{
    int spawnX = Raylib.GetRandomValue(-800, 800);
    enemy[i] = scene.CreateGameObject(new Vector2(player.position.X - spawnX, player.position.Y - 400), new Vector2(50,50), Color.Blue, 100, 1);
    enemy[i].isEnemy = true;
}

GameObject[] Projectiles = new GameObject[2];

Projectiles[0] = scene.CreateGameObject(new Vector2 (player.position.X, player.position.Y+5), new Vector2(10,70), Color.Green, 400, 0);
Projectiles[0].isProjectile = true;

Projectiles[1] = scene.CreateGameObject(new Vector2 (player.position.X+40, player.position.Y+5), new Vector2(10,70), Color.Green, -400, 0);
Projectiles[1].isProjectile = true;


while(!Raylib.WindowShouldClose())
{
    scene.Update();

    camera.Target=new Vector2(player.position.X, player.position.Y);
 
    //colision enemigo-personaje
    for(int i=0; i<20; i++)
    {
        if( enemy[i].position.X + enemy[i].size.X > player.position.X &&
            enemy[i].position.X < player.position.X + player.size.X &&
            enemy[i].position.Y + enemy[i].size.Y > player.position.Y &&
            enemy[i].position.Y < player.position.Y + player.size.Y
        )
        {
            enemy[i].Dead();
            player.vidas--;
        }
    }
    //colision enemigo-proyectil
    for(int i=0; i<20; i++)
    {
        for(int j=0; j<2; j++)
        {
            if( enemy[i].position.X + enemy[i].size.X > Projectiles[j].position.X &&
                enemy[i].position.X < Projectiles[j].position.X + Projectiles[j].size.X &&
                enemy[i].position.Y + enemy[i].size.Y > Projectiles[j].position.Y &&
                enemy[i].position.Y < Projectiles[j].position.Y + Projectiles[j].size.Y
            )
            {
                enemy[i].Dead();
            }
        }
    }

    Raylib.ClearBackground(Color.Black);
    Raylib.BeginDrawing();
    Raylib.BeginMode2D(camera);

    if(player.vidas>0)
    {
        scene.Draw();
    }
    else
    {
        Raylib.DrawText("Dead", (int) player.position.X-30, (int) player.position.Y, 60, Color.Red);
    }

    Raylib.EndMode2D();
    Raylib.EndDrawing();
}

//posibles mejoras: 
// Spawn de los enemigos no solo arriba
// añadir sprites


/*

struct son clases que funcionan por copia en vez de por puntero. 
Los tipos matematicos normalmente son sctructs. 

*/