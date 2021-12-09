using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TilesManager : MonoBehaviour
{

    public Point GridPosition { get;  set; }
 
    public void Setup(Point gridpos, Vector3 worldpos, Transform Folder)
    {
        // put tiles in a folder when loaded
        transform.SetParent(Folder);

        //set grid position 
        GridPosition = gridpos;

        //set world pos
        transform.position = worldpos;

        // spreads the tiles out
        transform.localScale = Vector3.one * (0.5f);
        
        //Debug.Log(gridpos);

        // store the cords in the dictionary created in LevelSetup
        LevelSetup.Instance.Tiles.Add(gridpos, this);
    }

    private void OnMouseOver()
    {
      //  Debug.Log(GridPosition.X + "  " + GridPosition.Y);

        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.Clickedbutton != null)
        {
            if (Input.GetMouseButtonDown(0))
         {
                Placement();
                
         }


         }
    }
    private void OnMouseDown()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began && ToggleBarrackUi.Instance.Clickedbutton != null))
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("There's touch");
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {

                        if (hit.transform.tag.Equals("Grass") && GmRef.Instance.unitOnField != GmRef.Instance.Availableunit)
                        {
                            Debug.Log("hitGrass");
                            Debug.Log(GridPosition.X + "  " + GridPosition.Y);
                            Placement();
                            break;
                        }
                        else
                        {
                            Debug.Log("Didn't hit grass");
                            Debug.Log(GridPosition.X + "  " + GridPosition.Y);
                            break;
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
      
      
    }
    private void Placement()
    {
        Debug.Log("Placement");
        GameObject tmp = Instantiate(ToggleBarrackUi.Instance.Clickedbutton.Unitprefab, transform.position, Quaternion.identity);
     //   tmp.GetComponent<MeshRenderer>().sortingOrder = GridPosition.Y;
      //  tmp.transform.SetParent(transform);
        ToggleBarrackUi.Instance.PlaceUnit();
        GmRef.Instance.unitOnField += 1;
    }
}