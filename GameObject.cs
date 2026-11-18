
using Raylib_cs;
using System.Numerics;

public class GameObject
{
    public Vector2 position;
    public Vector2 size;
    public Color color;
    public int speed;
    public int vidas;

    public int shootTimer=0;

    public bool isPlayer;
    public bool isEnemy;
    public bool isProjectile;

    public static GameObject Player;
    public static GameObject[] Enemy;
    public static GameObject[] Projectiles; 

    public GameObject(Vector2 position, Vector2 size, Color color, int speed, int vidas)
    {
        this.position = position;
        this.size = size;
        this.color = color;
        this.speed = speed;
        this.vidas = vidas;
    }


    public void Update()
    {

        if(isPlayer){
            if(Raylib.IsKeyDown(KeyboardKey.Right))
            {
                this.position.X += this.speed*Raylib.GetFrameTime();
            }
            if(Raylib.IsKeyDown(KeyboardKey.Left))
            {
                this.position.X -= this.speed*Raylib.GetFrameTime();
            }
            if(Raylib.IsKeyDown(KeyboardKey.Up))
            {
                this.position.Y -= this.speed*Raylib.GetFrameTime();
            }
            if(Raylib.IsKeyDown(KeyboardKey.Down))
            {
                this.position.Y += this.speed*Raylib.GetFrameTime();
            }
        }
        else if(isEnemy)
        {
            
            Vector2 direccion = new Vector2(this.position.X - Player.position.X, this.position.Y - Player.position.Y );
            direccion = Vector2.Normalize(direccion);

            this.position -= direccion*Raylib.GetFrameTime()*speed;;
             
        
        }
        else if(isProjectile)
        {
            if(GameObject.CanShoot())
            this.position.X += this.speed;
            if(this.position.X < Player.position.X-200 || this.position.X > Player.position.X+250)
            {
                this.position.X = Player.position.X;
            }
        }
        
    }

    public void Draw()
    {
        Raylib.DrawRectangle(
            (int) this.position.X,
            (int) this.position.Y,
            (int) this.size.X,
            (int) this.size.Y,
            color
        );

        if(isPlayer)
        {
            Raylib.DrawRectangle(
                (int) this.position.X,
                (int) this.position.Y + 60,
                (int) 5 * this.vidas,
                (int) 5,
                Color.Red
            );
        }

        Player.shootTimer ++;
    }


    public void Dead()
    {
        if(!isPlayer)
        {
            this.position.X = Raylib.GetRandomValue(-800, 800);
            this.position.Y -= 800; 
        }
    }

    
    public static bool CanShoot()
    {
        float tiempo = Player.shootTimer *  Raylib.GetFrameTime();
        Console.WriteLine(tiempo);
        if(tiempo>=50)
        { 
            Player.shootTimer = 0;
            return true;
        }
        else return false;//creo que esto es redundante ns 
    } 


}