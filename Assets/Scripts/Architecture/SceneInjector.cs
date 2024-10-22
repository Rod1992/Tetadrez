//Copyright © 2024 Rodrigo Martin <rodrigomartin9669@gmail.com>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInjector : MonoBehaviour
{
    [SerializeField]
    ConfigPieces configPieces;

    // Start is called before the first frame update
    void Start()
    {
        Game game = new Game(configPieces);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
