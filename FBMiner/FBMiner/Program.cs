using System;
using System.Xml;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ReadToken(); //Reads the Access Token from a predefined TXT file
            Start:
            var facebookClient = new FacebookClient();
            var facebookService = new FacebookService(facebookClient);
            var getAccountTask = facebookService.GetAccountAsync(FacebookSettings.AccessToken);
            Task.WaitAll(getAccountTask);
            var account = getAccountTask.Result;
            Console.WriteLine($"Connected to account {account.Name}!");
            
            // !New method for navigation!
            int userInputMain = 0;
            do
            {
                userInputMain = DisplayMenu();
                switch (userInputMain)
                {
                    case 1:
                        {
                            Console.Clear();

                            Console.WriteLine($"\nAccount ID: {account.Id} \nFull name: {account.Name} \nEmail {account.Email} " +
                                              $"\nGender: {account.Gender} \nBirthday: {account.Birthday} \nLanguage: {account.Locale} " +
                                              $"\nLocation: {account.Location} \nWebsite: {account.Website}");

                            int userInputAccount = 0;
                            do
                            {
                                userInputAccount = AccountMenu();
                                switch (userInputAccount)
                                {
                                    case 1:
                                        ChangeToken();
                                        goto Start;
                                    case 2:
                                        DeleteToken();
                                        break;
                                    case 3:
                                        Console.WriteLine("\nWaiting for a valid option ...");
                                        break;
                                    default:
                                        Console.Clear();
                                        break;

                                }
                            } while (userInputAccount != 9);
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();

                            //Original Json Data
                            //Console.WriteLine($"Latest Posts: {account.Feed}");

                            Console.WriteLine("\nDisplaying this user's feed in chronological order:\n");

                            JArray sortedByTime = new JArray(account.Feed.OrderBy(obj => obj["created_time"]));

                            foreach (JToken m in sortedByTime)
                            {
                                Console.WriteLine("Story: " + m["story"]);
                                Console.WriteLine("Created time: " + m["created_time"]);
                                Console.WriteLine("Description: " + m["description"]);
                                Console.WriteLine("Link: " + m["link"]);
                                Console.WriteLine();
                            }

                            Console.WriteLine("Wrote these in an XML file!");

                            var xml = System.Xml.Linq.XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(System.Text.Encoding.ASCII.GetBytes(sortedByTime.ToString()), new XmlDictionaryReaderQuotas()));
                            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("documents/pagesSortedByCreation.xml"))
                            {
                                writetext.WriteLine(xml);
                            }

                            break;
                        }
                    case 3:
                        {
                            Console.Clear();

                            //Original Json Data
                            //Console.WriteLine($"Likes: {account.Likes}");

                            Console.WriteLine("\nDisplaying a list of user endorsed pages: \n");

                            foreach (JToken m in account.Likes)
                            {
                                Console.WriteLine("Page ID: " + m["id"]);
                                Console.WriteLine("Name of page: " + m["name"]);
                                Console.WriteLine("No of likes: " + m["fan_count"]);
                                Console.WriteLine("Created time: " + m["created_time"]);
                                Console.WriteLine("Description: " + m["about"]);
                                Console.WriteLine();
                            }

                            int userInputSort = 0;
                            do
                            {
                                userInputSort = SortingMenu();
                                switch (userInputSort) {
                                    case 1:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Pages sorted by ID (Ascending): \n");

                                            JArray sortedByID = new JArray(account.Likes.OrderBy(obj => obj["id"]));

                                            foreach (JToken m in sortedByID)
                                            {
                                                Console.WriteLine("Page ID: " + m["id"]);
                                                Console.WriteLine("Name of page: " + m["name"]);
                                                Console.WriteLine("No of likes: " + m["fan_count"]);
                                                Console.WriteLine("Created time: " + m["created_time"]);
                                                Console.WriteLine("Description: " + m["about"]);
                                                Console.WriteLine();
                                            }

                                            Console.WriteLine("Wrote these in an XML file!");

                                            var xml = System.Xml.Linq.XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(System.Text.Encoding.ASCII.GetBytes(sortedByID.ToString()), new XmlDictionaryReaderQuotas()));
                                            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("documents/pagesSortedByID.xml"))
                                            {
                                                writetext.WriteLine(xml);
                                            }

                                            break;
                                        }
                                    case 2:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Pages sorted by name (Alphabetical): \n");

                                            JArray sortedByName = new JArray(account.Likes.OrderBy(obj => obj["name"]));

                                            foreach (JToken m in sortedByName)
                                            {
                                                Console.WriteLine("Page ID: " + m["id"]);
                                                Console.WriteLine("Name of page: " + m["name"]);
                                                Console.WriteLine("No of likes: " + m["fan_count"]);
                                                Console.WriteLine("Created time: " + m["created_time"]);
                                                Console.WriteLine("Description: " + m["about"]);
                                                Console.WriteLine();
                                            }

                                            Console.WriteLine("Wrote these in an XML file!");

                                            var xml = System.Xml.Linq.XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(System.Text.Encoding.ASCII.GetBytes(sortedByName.ToString()), new XmlDictionaryReaderQuotas()));
                                            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("documents/pagesSortedByName.xml"))
                                            {
                                                writetext.WriteLine(xml);
                                            }

                                            break;
                                        }
                                    case 3:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Pages sorted by number of fans/likes (Ascending): \n");

                                            JArray sortedByFansAscending = new JArray(account.Likes.OrderBy(obj => obj["fan_count"]));

                                            foreach (JToken m in sortedByFansAscending)
                                            {
                                                Console.WriteLine("Page ID: " + m["id"]);
                                                Console.WriteLine("Name of page: " + m["name"]);
                                                Console.WriteLine("No of likes: " + m["fan_count"]);
                                                Console.WriteLine("Created time: " + m["created_time"]);
                                                Console.WriteLine("Description: " + m["about"]);
                                                Console.WriteLine();
                                            }

                                            Console.WriteLine("Wrote these in an XML file!");

                                            var xml = System.Xml.Linq.XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(System.Text.Encoding.ASCII.GetBytes(sortedByFansAscending.ToString()), new XmlDictionaryReaderQuotas()));
                                            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("documents/pagesSortedByFansAsc.xml"))
                                            {
                                                writetext.WriteLine(xml);
                                            }

                                            break;
                                        }
                                    case 4:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Pages sorted by number of fans/likes (Descending): \n");

                                            JArray sortedByFansDescending = new JArray(account.Likes.OrderByDescending(obj => obj["fan_count"]));

                                            foreach (JToken m in sortedByFansDescending)
                                            {
                                                Console.WriteLine("Page ID: " + m["id"]);
                                                Console.WriteLine("Name of page: " + m["name"]);
                                                Console.WriteLine("No of likes: " + m["fan_count"]);
                                                Console.WriteLine("Created time: " + m["created_time"]);
                                                Console.WriteLine("Description: " + m["about"]);
                                                Console.WriteLine();
                                            }

                                            Console.WriteLine("Wrote these in an XML file!");

                                            var xml = System.Xml.Linq.XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(System.Text.Encoding.ASCII.GetBytes(sortedByFansDescending.ToString()), new XmlDictionaryReaderQuotas()));
                                            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("documents/pagesSortedByFansDes.xml"))
                                            {
                                                writetext.WriteLine(xml);
                                            }

                                            break;
                                        }
                                    case 5:
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Pages sorted by page liked time (Chronological): \n");

                                            JArray sortedByCreationTime = new JArray(account.Likes.OrderByDescending(obj => obj["created_time"]));

                                            foreach (JToken m in sortedByCreationTime)
                                            {
                                                Console.WriteLine("Page ID: " + m["id"]);
                                                Console.WriteLine("Name of page: " + m["name"]);
                                                Console.WriteLine("No of likes: " + m["fan_count"]);
                                                Console.WriteLine("Created time: " + m["created_time"]);
                                                Console.WriteLine("Description: " + m["about"]);
                                                Console.WriteLine();
                                            }

                                            Console.WriteLine("Wrote these in an XML file!");

                                            var xml = System.Xml.Linq.XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(System.Text.Encoding.ASCII.GetBytes(sortedByCreationTime.ToString()), new XmlDictionaryReaderQuotas()));
                                            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("documents/pagesSortedByCreation.xml"))
                                            {
                                                writetext.WriteLine(xml);
                                            }

                                            break;
                                        }
                                    case 6:
                                        {
                                            Console.WriteLine("\nWaiting for a valid option ... ");
                                            break;
                                        }
                                    default:
                                        Console.Clear();
                                        break;
                                }
                            } while (userInputSort != 9);
                            
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();

                            //Console.WriteLine($"\nUsers with Graph App installed on their account: {account.Friends}");

                            JArray sortedByName = new JArray(account.Friends.OrderBy(obj => obj["name"]));
                            foreach (JToken m in sortedByName)
                            {
                                Console.WriteLine("User ID: " + m["id"]);
                                Console.WriteLine("Name: " + m["name"]);
                                Console.WriteLine();
                            }
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();

                            Console.Write("Type the message you would like to be posted: ");
                            string messageToPost=Console.ReadLine();
                            var postOnWallTask = facebookService.PostOnWallAsync(FacebookSettings.AccessToken, messageToPost);
                            Task.WaitAll(postOnWallTask);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nWaiting for a valid option ... ");
                            break;
                        }
                }
            } while (userInputMain != 9);
            
        }
        
        static public int DisplayMenu()
        {
            Console.WriteLine("\nFacebook Miner Menu:");
            Console.WriteLine("1. Account info and settings");
            Console.WriteLine("2. Display feed");
            Console.WriteLine("3. Show a list of liked pages");
            Console.WriteLine("4. Display data for other users");
            Console.WriteLine("5. Create a post (Testing purposes)");
            Console.WriteLine("6. [More]");
            Console.WriteLine("9. Exit");
            Console.Write("Input an option: ");
            var menuChoice = Console.ReadLine();
            return Convert.ToInt32(menuChoice);
        }

        static public int AccountMenu()
        {
            Console.WriteLine("\nAccount info and settings:");
            Console.WriteLine("1. Change Access Token");
            Console.WriteLine("2. Delete Access Token");
            Console.WriteLine("3. [More]");
            Console.WriteLine("9. Back to main menu");
            Console.Write("Input an option: ");
            var menuChoice = Console.ReadLine();
            return Convert.ToInt32(menuChoice);
        }

        static public int SortingMenu()
        {
            Console.WriteLine("\nSorting Menu:");
            Console.WriteLine("1. Sort by \"id\" (Unique FB Page ID: Ascending)");
            Console.WriteLine("2. Sort by \"name\" (Page Name: Alphabetical)");
            Console.WriteLine("3. Sort by \"fan_count\" (Number of likes: Ascending)");
            Console.WriteLine("4. Sort by \"fan_count\" (Number of likes: Descending)");
            Console.WriteLine("5. Sort by \"created_time\" (Page Liked: Chronological)");
            Console.WriteLine("6. [More]");
            Console.WriteLine("9. Back to main menu");
            Console.Write("Input an option: ");
            var menuChoice = Console.ReadLine();
            return Convert.ToInt32(menuChoice);
        }

        static public void ReadToken()
        {
            string path = @"token\AccessToken.txt";

            if (File.Exists(path) && (File.ReadAllBytes(path).Length!=0))
            {
                FacebookSettings.AccessToken = File.ReadAllText(path);
            } else
            {
                Console.WriteLine("No Access Token found!");
                Console.WriteLine("The program needs an Access Token from Facebook to continue running. Please get a token by using the Graph API Explorer that Facebook provides at: https://developers.facebook.com/tools/explorer/");
                Console.WriteLine("Waiting for Access Token: ");
                FacebookSettings.AccessToken=Console.ReadLine();

                using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("token/AccessToken.txt"))
                {
                    writetext.Write(FacebookSettings.AccessToken);
                }
            }
        }

        static public void ChangeToken()
        {
            Console.Write("Input a new token: ");
            FacebookSettings.AccessToken = Console.ReadLine();
            using (System.IO.StreamWriter writetext = new System.IO.StreamWriter("token/AccessToken.txt"))
            {
                writetext.Write(FacebookSettings.AccessToken);
            }
            Console.Clear();
            Console.WriteLine("Access Token was changed!");
        }

        static public void DeleteToken()
        {
            string path = @"token\AccessToken.txt";
            File.Delete(path);
            Console.Clear();
            Console.WriteLine("Access Token Deleted!");
            Console.WriteLine("Next time you run this program, you will need to input a token!");
        }
    }    
}