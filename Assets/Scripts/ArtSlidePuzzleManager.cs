using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtSlidePuzzleManager : MonoBehaviour
{
    [Header("Results")]
    [SerializeField] private int tilesOnCorrectPlaces;

    [Header("Tiles")]
    [SerializeField] private List<SlidePuzzleTile> tiles;
    [SerializeField] private Transform emptySpace;

    [Header("Debugging")]
    private Camera cam;
    private List<SlidePuzzleTile> tilesTemp = new List<SlidePuzzleTile>();
    [SerializeField] private List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {
        /*foreach(Sprite art in artImages)
        {
            artImageTemp.Add(art);
        }

        // set the last tile to have the correct image, then hide the tile
        tiles[11].sprite = artImageTemp[11];
        artImageTemp.RemoveAt(11);

        foreach(SpriteRenderer tile in tiles)
        {
            if(tiles.IndexOf(tile) != 11)
            {
                int i = Random.Range(0, artImageTemp.Count);

                tile.sprite = artImageTemp[i];

                artImageTemp.RemoveAt(i);
            }
        }*/

        tiles[11].gameObject.SetActive(false);
        cam = Camera.main;

        foreach(SlidePuzzleTile tile in tiles)
        {
            if (tiles.IndexOf(tile) < 11)
            {
                tilesTemp.Add(tile);
            }
        }

        ShuffleTiles();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit)
            {
                if(Vector2.Distance(emptySpace.position, hit.transform.position) <= 3f)
                {
                    Vector2 lastEmptySpace = emptySpace.position;
                    emptySpace.position = hit.transform.position;

                    hit.transform.GetComponent<SlidePuzzleTile>().MoveToEmptySpace(lastEmptySpace);
                }
            }
        }
    }

    private void ShuffleTiles()
    {
        //List<Vector3> positions = new List<Vector3>();

        foreach(SlidePuzzleTile tile in tilesTemp)
        {
            positions.Add(tile.correctPosition);
        }

        for(int i = 0; i < 11; i++)
        {
            int newPos = Random.Range(0, positions.Count);
            int newTile = Random.Range(0, tilesTemp.Count);

            tilesTemp[newTile].transform.localPosition = positions[newPos];

            if(tilesTemp[newTile].correctPosition == positions[newPos])
            {
                tilesOnCorrectPlaces++;
            }

            positions.RemoveAt(newPos);
            tilesTemp.RemoveAt(newTile);
        }
    }

    public void UpdateProgress(int change)
    {
        tilesOnCorrectPlaces =+ change;

        if(tilesOnCorrectPlaces >= 11)
        {
            Debug.Log("You win!");
        }
    }
}
