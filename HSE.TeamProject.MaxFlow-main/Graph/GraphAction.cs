using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class GraphAction
    {
        /// <summary>
        /// Graph matrix
        /// </summary>
        private readonly byte[,] graph;

        /// <summary>
        /// Capacity of the rib
        /// </summary>
        private readonly int[,] rib_capacity;

        /// <summary>
        /// Current flow
        /// </summary>
        private int[,] curr_flow;

        /// <summary>
        /// Size of the graph
        /// </summary>
        private int size;

        /// <summary>
        /// Max flow in the graph
        /// </summary>
        private int max_flow;

        /// <summary>
        /// Check node for using
        /// </summary>
        private bool[] used;

        /// <summary>
        /// Flow to node[i] from the start 
        /// </summary>
        private int[] flow;

        /// <summary>
        /// Parent of the node[i]
        /// </summary>
        private int[] parent;

        /// <summary>
        /// Distance to the node[i] from start 
        /// </summary>
        private int[] dist; 

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="_graph">Graph matrix</param>
        public GraphAction(byte[,] _graph, int [,] _rib_capacity) : base() {
            graph = _graph;
            rib_capacity = _rib_capacity;
            max_flow = 0;
            size = graph.GetLength(0);

            used = new bool[size];
            flow = new int[size];
            parent = new int[size];
            dist = new int[size];
            curr_flow = new int[size,size];

            find_max_flow();
        }

        /// <summary>
        /// Get max flow in the graph
        /// </summary>
        public int GetMaxFlow => max_flow;

        /// <summary>
        /// Output graph matrix
        /// </summary>
        public void Output() {
            for (int row = 0; row < size; ++row) {
                for (int column = 0; column < size; ++column) {
                    Console.Write("{0}({1}) \t",graph[row, column], rib_capacity[row, column]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            for (int row = 0; row < size; ++row)
            {
                for (int column = 0; column < size; ++column)
                {
                    if (curr_flow[row, column] < 0) {
                        Console.Write("{0} \t", 0);
                    }
                    else {
                        Console.Write("{0} \t", curr_flow[row, column]);
                    }
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Find max flow in the graph
        /// </summary>
        private void find_max_flow() {
            int u, v;
            int s = 0, t = size - 1;
            max_flow = 0;
            while (bfs(s, t)) {
                int rib_add = flow[t];
                v = t;
                u = parent[v];
                while (v != s) {
                    curr_flow[u, v] += rib_add;
                    curr_flow[v, u] -= rib_add;
                    v = u;
                    u = parent[v];
                }

                max_flow += rib_add;
            }
        }

        /// <summary>
        /// Initialize 
        /// </summary>
        private void init() {
            for (int i = 1; i < size; ++i) {
                parent[i] = 0;
                used[i] = false;
                flow[i] = 0;
                dist[i] = Int32.MaxValue;
            }
        }

        /// <summary>
        /// Algorithm Bellman-Ford
        /// </summary>
        /// <param name="s">Start</param>
        /// <param name="t">End</param>
        /// <returns>Is using node[t]?</returns>
        private bool bfs(int s, int t) {
            init();
            Queue<int> Q = new Queue<int>();
            used[s] = true;
            parent[s] = s;
            flow[s] = Int32.MaxValue;

            Q.Enqueue(s);
            while (!used[t] && Q.Count != 0) {
                int u = Q.Dequeue();
                for (int node = 1; node < size; ++node) {
                    if (!used[node] && (rib_capacity[u,node] - curr_flow[u,node] > 0)) {
                        flow[node] = Math.Min(flow[u], rib_capacity[u, node] - curr_flow[u, node]);
                        used[node] = true;
                        parent[node] = u;
                        Q.Enqueue(node);
                    }
                }
            }

            return used[t];
        }
    }
}
