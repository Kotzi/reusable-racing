using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public GameController gameController;
    public int laps = 3;

    private Dictionary<string, (string, int, int, float)> positions = new Dictionary<string, (string, int, int, float)>();

    void LateUpdate()
    {
        var sortedPositions = new List<(string, int, int, float)>(this.positions.Values);
        sortedPositions.Sort(delegate((string, int, int, float) a, (string, int, int, float) b) {
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
        sortedPositions.Reverse();

        var text = "";
        foreach (var p in sortedPositions)
        {
            //text += $"{p.Item1} - {p.Item2} - {p.Item3} - {p.Item4}\n";
            text += $"{p.Item1}\n";
        }

        this.gameController.raceUICanvas.updateDebugText(text);
    }

    public void updatePosition(string id, string name, int laps, int closestWaypoint, float distanceToClosestWaypoint)
    {
        this.positions[id] = (name, laps, closestWaypoint, distanceToClosestWaypoint);
    }
}
