
using Raylib_cs;
using System.Numerics;

public class GameObject
{
    public Vector2 position;
    public Vector2 size;
    public Color color;
    public int speed;
    public int vidas;

    public float shootTimer=0;
    public bool isShooting=false; 
    private float lastPositionX;

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
            if(isShooting)
            { 
                Console.WriteLine(lastPositionX);
                Console.WriteLine(this.position.X);
                this.position.X += this.speed*Raylib.GetFrameTime();
                if(this.position.X < lastPositionX-90 || this.position.X > lastPositionX+90)
                {
                    isShooting=false; 
                } 
                shootTimer=0;
            }
            else
            {
                this.position.X = Player.position.X+20; 
                this.position.Y = Player.position.Y+5;
                shootTimer += Raylib.GetFrameTime();
                if(shootTimer>2f)
                {
                    isShooting=true;
                    lastPositionX = this.position.X;
                }
            }      
        }        
    }

    public void Draw()
    {
        if(!isProjectile || isProjectile && isShooting)
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
    }


    public void Dead()
    {
        if(!isPlayer)
        {
            this.position.X = Raylib.GetRandomValue((int)Player.position.X-800, (int)Player.position.X+800);
            this.position.Y -= 800; 
        }
    }
}