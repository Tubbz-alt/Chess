using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public static BoardManager Instance { set; get; }
    private bool[,] allowedMoves { set; get; }

    private const float TILE_SIZE = 1.0f;

    private const float TILE_OFFSET = 0.5f;

    private const int chessBoardDimension = 8;

    private int selectionX = -1;
    private int selectionZ = -1;

    public Chessman[,] Chessmans { set; get; }
    private Chessman selectedChessman;

    [SerializeField]
    [Tooltip("It decides if there is a white chance or black chance at the very first")]
    private bool isWhiteChance = true;

    [SerializeField]
    [Tooltip("The container which contain all the chessman prefabs")]
    private List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Material prevMat;
    public Material selectedMat;

    public int[] EnPassantMove { set; get; }

    // maximum distance till which we raycast from the main camera
    [SerializeField]
    [Tooltip("Maximum distance till which we raycast from the Main Camera")]
    private float maxDistance = 25.0f;

    void Start() {
        Instance = this;
        SpawnAllChessman();
    }

	// Update is called once per frame
	void Update () {
        UpdateSelection();
        DrawChessBoard();

        if (Input.GetMouseButtonDown(0)) {
            // If the click happened to be in the board then only it makes sense to respond to the input else simply ignore the input.
            if (selectionX >= 0 && selectionZ >= 0) {
                // If there was not a chessman which was already selected
                if (selectedChessman == null){
                    SelectChessman(selectionX, selectionZ);
                }
                // We have a chessman that it is already selected
                else {
                    MoveChessman(selectionX, selectionZ);
                }
            }
        }
	}

    // This function selects the chessman at the given position at its chance
    private void SelectChessman(int x, int z){
        if (Chessmans[x, z] != null && Chessmans[x, z].isWhite == isWhiteChance)
        {
            bool hasAtLeastOneMove = false;
            allowedMoves = Chessmans[x, z].PossibleMove();
            for (int i = 0; i < 8; i++){
                for (int j = 0; j < 8; j++) {
                    if(allowedMoves[i, j]) {
                        hasAtLeastOneMove = true;
                    }
                }
            }

            if (!hasAtLeastOneMove) {
                // dont even select this thing and simply return which is the reason why we wrote this thing in this function
                return;
            }

            selectedChessman = Chessmans[x, z];

            prevMat = selectedChessman.GetComponent<MeshRenderer>().material;
            selectedMat.mainTexture = prevMat.mainTexture;
            selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;

            BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
        }
        return;
    }
    
    // This function moves the chessman to the given position if its possible
    
        // TODO: katna and all
    private void MoveChessman(int x, int z) {

        if (allowedMoves[x, z]) {

            Chessman c = Chessmans[x, z];
            // This means that there is a piece where we are going
            if (c != null && c.isWhite != isWhiteChance) {
                // Capture the piece

                // If the piece is the king
                if (c.GetType() == typeof(King)) {
                    // END THE GAME
                    EndGame();
                    return;
                }

                activeChessman.Remove(c.gameObject);    // Updating our active chessmans list
                Destroy(c.gameObject);
            }

            // This has to be done before resetting
            if (x == EnPassantMove[0] && z == EnPassantMove[1]) {
                if (isWhiteChance){            // White turn basically
                    c = Chessmans[x, z - 1];
                }
                else {                  // Black turn basically
                    c = Chessmans[x, z + 1];
                }

                activeChessman.Remove(c.gameObject);    // Updating our active chessmans list
                Destroy(c.gameObject);
            }

            // Resetting is important here
            EnPassantMove[0] = -1;
            EnPassantMove[1] = -1;
            if (selectedChessman.GetType() == typeof(Pawn)) {

                if (z == 7){
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    SpawnChessman(3, x, z);
                    selectedChessman = Chessmans[x, z];
                }
                else if (z == 0) {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    SpawnChessman(9, x, z);
                    selectedChessman = Chessmans[x, z];
                }

                if (selectedChessman.CurrentY == 1 && z == 3){
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = z - 1;
                }
                else if (selectedChessman.CurrentY == 6 && z == 4) {
                    EnPassantMove[0] = x;
                    EnPassantMove[1] = z + 1;
                }
            }


            // Updating the from-moved position in our 2d chessmans array
            // This is equivalent to picking up the chessman from that position
            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;

            // This is equivalent to the physical movement of the chess piece
            selectedChessman.transform.position = GetTileCenter(x, z);

            // Setting the position in terms of memory now adding to the physical position change
            selectedChessman.SetPosition(x, z);

            // Updating the to-moved position in our 2d chessmans array
            // This is equivalent to placing the chessman to that position
            Chessmans[x, z] = selectedChessman;
            
            // alternating the chance
            isWhiteChance = !isWhiteChance;
        }

        // resetting our material again
        selectedChessman.GetComponent<MeshRenderer>().material = prevMat;
        // selected chessman should always be reset to null after the movement function be it a valid move or not
        selectedChessman = null;
        // also the highlights needs to be hidden
        BoardHighlights.Instance.HideHighlights();
    }

    void UpdateSelection() {
        // if main camera not there
        if (!Camera.main)
            return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, maxDistance, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionZ = (int)hit.point.z;
        }
        else {
            selectionX = -1;
            selectionZ = -1;
        }
    }   

    // this function is not actually used in the final build but just for debugging
    void DrawChessBoard() {
       
        Vector3 width = Vector3.right * chessBoardDimension;
        Vector3 height = Vector3.forward * chessBoardDimension;

        // drawing widthLines
        for (int i = 0; i <= 8; i++) {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + width);
        }

        // draw heightLines
        for (int i = 0; i <= 8; i++) {
            Vector3 start = Vector3.right * i;
            Debug.DrawLine(start, start + height);
        }

        // if selection is not outside the board it gets drawn 
        if (selectionX >= 0 && selectionZ >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionZ + Vector3.right * selectionX,
                                Vector3.forward * (selectionZ + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(Vector3.forward * (selectionZ + 1) + Vector3.right * selectionX,
                                Vector3.forward * selectionZ + Vector3.right * (selectionX + 1));
                
        }

    }

    // Spawns all the Chessman
    // TODO: Instead of hardcoded values we can use variables for better code
    void SpawnAllChessman() {

        activeChessman = new List<GameObject> ();
        Chessmans = new Chessman[8, 8];
        EnPassantMove = new int[2] {-1, -1};

        // ------------- White Team

        // Rooks
        SpawnChessman(0, 0, 0);
        SpawnChessman(0, 7, 0);

        // Knights
        SpawnChessman(1, 1, 0);
        SpawnChessman(1, 6, 0);

        // Bishops
        SpawnChessman(2, 2, 0);
        SpawnChessman(2, 5, 0);

        // Queen
        SpawnChessman(3, 3, 0);

        // King
        SpawnChessman(4, 4, 0);

        // Pawns
        for (int i = 0; i <= 7; i++) {
            SpawnChessman(5, i, 1);
        }

        // -------------  Black Team

        // Rooks
        SpawnChessman(6, 0, 7);
        SpawnChessman(6, 7, 7);

        // Knights
        SpawnChessman(7, 1, 7);
        SpawnChessman(7, 6, 7);

        // Bishops
        SpawnChessman(8, 2, 7);
        SpawnChessman(8, 5, 7);

        // Queen
        SpawnChessman(9, 4, 7);

        // King
        SpawnChessman(10, 3, 7);

        // Pawns
        for (int i = 0; i <= 7; i++)
        {
            SpawnChessman(11, i, 6);
        }
    }


    void SpawnChessman(int index , int x, int z) {
        GameObject spawningChessman = Instantiate(chessmanPrefabs[index], GetTileCenter(x, z), chessmanPrefabs[index].transform.rotation) as GameObject;
        spawningChessman.transform.SetParent(transform);
        Chessmans[x, z] = spawningChessman.GetComponent<Chessman>();
        Chessmans[x, z].SetPosition(x, z);
        activeChessman.Add(spawningChessman);
    }  

    private Vector3 GetTileCenter(int x, int z) {

        Vector3 origin = Vector3.zero;
        origin.x += TILE_SIZE * x + TILE_OFFSET ;
        origin.z += TILE_SIZE * z + TILE_OFFSET ;
        return origin;
    }

    private void EndGame() {
        if (isWhiteChance){
            Debug.Log("White team wins");
        }
        else {
            Debug.Log("Black team wins");
        }

        foreach (GameObject go in activeChessman) {
            Destroy(go);
        }

        isWhiteChance = true;               // or maybe flip it for the next game if you want to
        BoardHighlights.Instance.HideHighlights();
        SpawnAllChessman();
    }
}
