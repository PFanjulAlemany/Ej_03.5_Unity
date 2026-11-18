using System.Numerics;
using Raylib_cs;

public class Scene
{
    public GameObject[] gameObjects;
    public int count;
    public static int MaxObjects = 50;

    public GameObject CreateGameObject(Vector2 position, Vector2 size, Color color, int speed, int vidas)
    {
        if(gameObjects == null)
        {
            gameObjects = new GameObject[MaxObjects];
        }

        GameObject newObject = new GameObject(position, size, color, speed, vidas);
        gameObjects[count] = newObject;
        count += 1;
        return newObject;
    } 

    public void Draw()
    {
        for(int i=0; i<count; i++)
        {
            gameObjects[i].Draw();
        }
    }

    public void Update()
    {
        for(int i=0; i<count; i++)
        {
            gameObjects[i].Update();
        }
    }
}