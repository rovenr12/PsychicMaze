using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Maze : MonoBehaviour {
    [SerializeField] int width = 30;
    [SerializeField] int depth = 30;
    [SerializeField] int scale = 7;
    [SerializeField] int angelAmount = 10;
    [SerializeField] GameObject straight;
    [SerializeField] GameObject corner;
    [SerializeField] GameObject crossroad;
    [SerializeField] GameObject end;
    [SerializeField] GameObject tJunction;
    [SerializeField] GameObject player;
    [SerializeField] GameObject angel;
    [SerializeField] GameObject endPoint;

    List<MapLocation> directions = new() {
        new MapLocation(1, 0),
        new MapLocation(0, 1),
        new MapLocation(-1, 0),
        new MapLocation(0, -1)
    };

    byte[,] map;
    List<MapLocation> deadEnds = new();

    void Start() {
        InitialiseMap();
        Generate();
        DrawMap();
        PlaceNPC();
        PlaceAngels();
        PlaceEndPoint();
    }

    void PlaceNPC() {
        for (int z = 0; z < depth; z++) {
            for (int x = 0; x < width; x++) {
                if (map[x, z] == 0) {
                    player.transform.position = new Vector3(x * scale, 0.5f, z * scale);
                    return;
                }
            }
        }
    }
    
    void PlaceEndPoint() {
        for (int z = depth - 1; z > 0; z--) {
            for (int x = width - 1; x > 0; x--) {
                if (map[x, z] == 0) {
                    endPoint.transform.position = new Vector3(x * scale, 0.2f, z * scale);
                    return;
                }
            }
        }
    }

    void PlaceAngels() {
        if (angelAmount >= deadEnds.Count) {
            foreach (MapLocation loc in deadEnds) {
                Instantiate(angel, new Vector3(loc.x * scale, 0f, loc.z * scale), Quaternion.identity);
            }

            angelAmount = deadEnds.Count;
        } else {
            for (int i = 0; i < angelAmount; i++) {
                int idx = Random.Range(0, deadEnds.Count);
                MapLocation loc = deadEnds[idx];
                GameObject instantiate = Instantiate(angel, new Vector3(loc.x * scale, 0f, loc.z * scale), Quaternion.identity);
                instantiate.transform.Rotate(new Vector3(-90, 0, 0));
                deadEnds.RemoveAt(idx);
            }
        }
    }

    void InitialiseMap() {
        map = new byte[width, depth];
        for (int z = 0; z < depth; z++)
        for (int x = 0; x < width; x++) {
            map[x, z] = 1; //1 = wall  0 = corridor
        }
    }

    // Start is called before the first frame update
    void Generate() {
        Generate(Random.Range(1, width - 1), Random.Range(1, depth - 1));
    }

    void Generate(int x, int z) {
        if (CountSquareNeighbours(x, z) >= 2) return;
        map[x, z] = 0;

        ShuffleDirection();
        for (int i = 0; i < directions.Count; i++) {
            Generate(x + directions[i].x, z + directions[i].z);
        }
    }

    void DrawMap() {
        for (int z = 0; z < depth; z++) {
            for (int x = 0; x < width; x++) {
                Vector3 pos = new Vector3(x * scale, 0, z * scale);
                if (PlaceStraight(x, z, pos)) continue;
                if (PlaceCrossRoad(x, z, pos)) continue;
                if (PlaceDeadEnd(x, z, pos)) continue;
                if (PlaceCorner(x, z, pos)) continue;
                PlaceTJunction(x, z, pos);
            }
        }
    }

    bool PlaceStraight(int x, int z, Vector3 pos) {
        if (Search2D(x, z, new[] {5, 0, 5, 1, 0, 1, 5, 0, 5})) {
            GameObject instantiate = Instantiate(straight, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, 90, 0);
        } else if (Search2D(x, z, new[] {5, 1, 5, 0, 0, 0, 5, 1, 5})) {
            Instantiate(straight, pos, Quaternion.identity);
        } else {
            return false;
        }

        return true;
    }

    bool PlaceCrossRoad(int x, int z, Vector3 pos) {
        if (Search2D(x, z, new[] {1, 0, 1, 0, 0, 0, 1, 0, 1})) {
            Instantiate(crossroad, pos, Quaternion.identity);
            return true;
        }

        return false;
    }

    bool PlaceDeadEnd(int x, int z, Vector3 pos) {
        if (Search2D(x, z, new[] {5, 1, 5, 0, 0, 1, 5, 1, 5})) {
            GameObject instantiate = Instantiate(end, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, 90, 0);
        } else if (Search2D(x, z, new[] {5, 1, 5, 1, 0, 0, 5, 1, 5})) {
            GameObject instantiate = Instantiate(end, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, -90, 0);
        } else if (Search2D(x, z, new[] {5, 1, 5, 1, 0, 1, 5, 0, 5})) {
            Instantiate(end, pos, Quaternion.identity);
        } else if (Search2D(x, z, new[] {5, 0, 5, 1, 0, 1, 5, 1, 5})) {
            GameObject instantiate = Instantiate(end, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, 180, 0);
        } else if (Search2D(x, z, new[] {5, 0, 5, 1, 0, 1, 5, 1, 5})) {
            GameObject instantiate = Instantiate(end, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, 180, 0);
        } else {
            return false;
        }

        deadEnds.Add(new MapLocation(x, z));
        return true;
    }

    bool PlaceCorner(int x, int z, Vector3 pos) {
        if (Search2D(x, z, new[] {5, 1, 5, 0, 0, 1, 1, 0, 5})) {
            GameObject instantiate = Instantiate(corner, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, 90, 0);
        } else if (Search2D(x, z, new[] {5, 1, 5, 1, 0, 0, 5, 0, 1})) {
            Instantiate(corner, pos, Quaternion.identity);
        } else if (Search2D(x, z, new[] {5, 0, 1, 1, 0, 0, 5, 1, 5})) {
            GameObject instantiate = Instantiate(corner, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, -90, 0);
        } else if (Search2D(x, z, new[] {1, 0, 5, 5, 0, 1, 5, 1, 5})) {
            GameObject instantiate = Instantiate(corner, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, 180, 0);
        } else {
            return false;
        }

        return true;
    }

    bool PlaceTJunction(int x, int z, Vector3 pos) {
        if (Search2D(x, z, new[] {1, 0, 1, 0, 0, 0, 5, 1, 5})) {
            GameObject instantiate = Instantiate(tJunction, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, 180, 0);
        } else if (Search2D(x, z, new[] {5, 1, 5, 0, 0, 0, 1, 0, 1})) {
            Instantiate(tJunction, pos, Quaternion.identity);
        } else if (Search2D(x, z, new[] {1, 0, 5, 0, 0, 1, 1, 0, 5})) {
            GameObject instantiate = Instantiate(tJunction, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, 90, 0);
        } else if (Search2D(x, z, new[] {5, 0, 1, 1, 0, 0, 5, 0, 1})) {
            GameObject instantiate = Instantiate(tJunction, pos, Quaternion.identity);
            instantiate.transform.Rotate(0, -90, 0);
        } else {
            return false;
        }

        return true;
    }

    bool Search2D(int c, int r, int[] pattern) {
        if (map[c, r] == 1) {
            return false;
        }

        int count = 0;
        int pos = 0;

        for (int z = 1; z > -2; z--) {
            for (int x = -1; x < 2; x++) {
                if (pattern[pos] == map[c + x, r + z] || pattern[pos] == 5) {
                    count++;
                }

                pos++;
            }
        }

        return count == 9;
    }

    int CountSquareNeighbours(int x, int z) {
        int count = 0;
        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1) return 5;
        if (map[x - 1, z] == 0) count++;
        if (map[x + 1, z] == 0) count++;
        if (map[x, z + 1] == 0) count++;
        if (map[x, z - 1] == 0) count++;
        return count;
    }

    void ShuffleDirection() {
        int n = directions.Count;
        while (n > 1) {
            n--;
            int k = Random.Range(0, directions.Count);
            (directions[k], directions[n]) = (directions[n], directions[k]);
        }
    }
}