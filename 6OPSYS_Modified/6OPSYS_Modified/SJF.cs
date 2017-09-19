using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6OPSYS_Modified
{
    class SJF
    {
        List<Job> jobs = new List<Job>();
        int idleCount = 1;
        Dictionary<String, int> ganttChart = new Dictionary<string, int>();
        List<Job> readyQueue = new List<Job>();
        public void addData(int i, int at, int bt)
        {
            Job incomingJobs = new Job();
            incomingJobs.processID = i;
            incomingJobs.arrivalTime = at;
            incomingJobs.burstTime = bt;
            jobs.Add(incomingJobs);
        }

        public void scheduleIt()
        {
            Display frm = new Display();
            int idleTime = 0;
            bool isProcessing = false;
            int processingTime = 0;
            for (int time = 0; ; time++)
            {
                if (jobs.Count == 0 && !isProcessing)
                {
                    break;
                }

                List<int> jobArrivals = checkJobsArrived(time, jobs);
                int jobCount = jobArrivals.Count;

                if (jobCount > 0)
                {
                    foreach (int jobArrival in jobArrivals)
                    {
                        int index = findIndex(jobs, jobArrival);
                        readyQueue.Add(jobs[index]);
                        jobs.RemoveAt(index);
                    }

                    readyQueue.Sort(new ShortestJobFirst());
                }

                if (!isProcessing)
                {
                    if (readyQueue.Count == 0)
                    {
                        idleTime++;
                    }

                    else
                    {
                        if (idleTime != 0)
                        {
                            ganttChart.Add("IDLE" + idleCount, idleTime);
                            idleCount++;
                            idleTime = 0;
                        }

                        ganttChart.Add((readyQueue[0].processID + 1).ToString(), readyQueue[0].burstTime);
                        processingTime = readyQueue[0].burstTime; //eto
                        readyQueue.RemoveAt(0);
                        isProcessing = true;
                    }
                }

                else
                {
                    processingTime--;
                    if (processingTime == 0)
                    {
                        if (readyQueue.Count == 0)
                        {
                            isProcessing = false;
                            idleTime++;
                        }

                        else
                        {
                            ganttChart.Add((readyQueue[0].processID + 1).ToString(), readyQueue[0].burstTime);
                            processingTime = readyQueue[0].burstTime; //eto
                            readyQueue.RemoveAt(0);
                            isProcessing = true;
                        }
                    }
                }
            }
            foreach (var jobProcessed in ganttChart)
            {
                frm.addtoGrid(jobProcessed.Key, jobProcessed.Value);
                //Console.WriteLine("Process id: " + jobProcessed.Key);
                //Console.WriteLine("Burst time: " + jobProcessed.Value);
                //Console.WriteLine();
            }
            frm.Show();
        }
        public static List<int> checkJobsArrived(int time, List<Job> jobs)
        {
            List<int> jobsIndex = new List<int>();
            int numberOfJobs = jobs.Count;

            for (int index = 0; index < numberOfJobs; index++)
            {
                if (jobs[index].arrivalTime == time)
                {
                    jobsIndex.Add(jobs[index].processID);
                }
            }
            return jobsIndex;
        }

        public static int findIndex(List<Job> jobs, int id)
        {
            int jobCount = jobs.Count;
            for (int index = 0; index < jobCount; index++)
            {
                if (jobs[index].processID == id)
                {
                    return index;
                }
            }
            return 0;
        }
    }

    class ShortestJobFirst : Comparer<Job>
    {
        public override int Compare(Job x, Job y)
        {
            if (x.burstTime.CompareTo(y.burstTime) > 0 || x.burstTime.CompareTo(y.burstTime) < 0)
            {
                return x.burstTime.CompareTo(y.burstTime);
            }

            else //equal la
            {
                return x.processID.CompareTo(y.processID);
            }
        }
    }
}
