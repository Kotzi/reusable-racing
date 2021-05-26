using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public GameController gameController;
    public Transform playerStartPosition;
    public Transform enemy1StartPosition;
    public Transform enemy2StartPosition;
    public Transform enemy3StartPosition;
    public Waypoint[] waypoints;
    public List<(string, int, int, float, string)> sortedPositions { get; private set; } = new List<(string, int, int, float, string)>();

    public int laps = 3;

    private Dictionary<string, (string, int, int, float, string)> positions = new Dictionary<string, (string, int, int, float, string)>();

    void LateUpdate()
    {
        this.sortedPositions = new List<(string, int, int, float, string)>(this.positions.Values);
        this.sortedPositions.Sort(delegate((string, int, int, float, string) a, (string, int, int, float, string) b) {
            var laps = a.Item2.CompareTo(b.Item2);
            if (laps != 0)
            {
                return laps;
            }
            else
            {
                var waypoint = a.Item3.CompareTo(b.Item3);
                if (waypoint != 0)
                {
                    return waypoint;
                }
                else
                {
                    return a.Item4.CompareTo(b.Item4);
                }
            }
        });
        this.sortedPositions.Reverse();

        this.gameController.raceUICanvas.updatePositions(this.sortedPositions);
    }

    public void updatePosition(string id, string name, int laps, int closestWaypoint, float distanceToClosestWaypoint)
    {
        this.positions[id] = (name, laps, closestWaypoint, distanceToClosestWaypoint, id);
    }

    public void enemyDied(string id)
    {
        this.positions.Remove(id);
    }
}
