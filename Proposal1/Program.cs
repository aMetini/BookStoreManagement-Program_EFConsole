using System;
using System.Collections.Generic;
using System.Linq;

namespace Proposal1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new TBMContext())
            {
                if (db.Database.CanConnect())
                {
                    String input;
                    do
                    {
                        ShowMenu();
                        input = Console.ReadLine().ToString();

                        switch (input)
                        {
                            case "1":
                                PrintISBNsForStockBalances(db);
                                break;
                            case "2":
                                PrintStoreNamesInStores(db);
                                break;
                            case "3":
                                ListStockBalanceForEachStore(db);
                                break;
                            case "4":
                                AddExistingBookToStoreBalance(db);
                                break;
                            case "5":
                                RemoveBookFromStoreBalance(db);
                                break;
                            case "6":
                                AddNewBookToDatabase(db);
                                break;
                            case "7":
                                AddNewAuthorToDatabase(db);
                                break;
                            case "8":
                                EditBookDetailsInDatabase(db);
                                break;
                            case "9":
                                EditAuthorDetailsInDatabase(db);
                                break;
                            case "10":
                                DeleteBookFromDatabase(db);
                                break;
                            case "11":
                                DeleteAuthorFromDatabase(db);
                                break;
                            case "q":
                            case "Q":
                                Console.WriteLine("Goodbye!");
                                break;
                            default:
                                Console.WriteLine("Error: Unknown option received - \'" + input + "\'");
                                break;
                        }
                    } while (input != "q" && input != "Q");
                }
                else Console.Error.WriteLine("Connection Failed!");
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nEnter a menu item or 'q' to quit:");
            Console.WriteLine("  1: List ISBNs in Stock Balances");
            Console.WriteLine("  2: List Store Names in Stores");
            Console.WriteLine("  3: List Stock Balances by Stores");
            Console.WriteLine("  4: Add Book to Store's Stock Balance");
            Console.WriteLine("  5: Remove Book from Store's Stock Balance");
            Console.WriteLine("  6: Add New Book to Database");
            Console.WriteLine("  7: Add New Author to Database");
            Console.WriteLine("  8: Edit Existing Book Details in Database");
            Console.WriteLine("  9: Edit Existing Author Details in Database");
            Console.WriteLine(" 10: Delete Existing Book from Database");
            Console.WriteLine(" 11: Delete Existing Author from Database");
            Console.Write("  > ");
        }

        static void PrintISBNsForStockBalances(TBMContext db)
        {
            foreach (var item in db.StockBalances.ToList())
            {
                Console.WriteLine(item.Isbn.ToString());
            }
        }

        static void PrintStoreNamesInStores(TBMContext db)
        {
            foreach (var store in db.Stores.ToList())
            {
                Console.WriteLine(store.StoreName);
            }
        }

        static void ListStockBalanceForEachStore(TBMContext db)
        {
            foreach (var store in db.Stores.ToList())
            {
                Console.WriteLine("\n" + store.StoreName);
                foreach (var item in db.StockBalances.ToList())
                {
                    if (item.BoutiqueId == store.Id)
                    {
                        Book book = null;
                        foreach (var tempBook in db.Books)
                        {
                            if (tempBook.Isbn13 == item.Isbn)
                            {
                                book = tempBook;
                                break;
                            }
                        }

                        if (book != null)
                        {
                            Console.WriteLine("  \"" + book.Title + "\" (ISBN: " + item.Isbn.ToString() + "): " + item.Number.ToString());
                        }
                        else
                        {
                            Console.WriteLine("  ISBN: " + item.Isbn.ToString() + ": " + item.Number.ToString());
                        }
                    }
                }
            }
        }

        static void AddExistingBookToStoreBalance(TBMContext db)
        {
            List<Book> booksList = GetListOfBooks(db);
            List<Store> storesList = GetListOfStores(db);
            int number;
            int input;

            if (booksList.Count < 1)
            {
                Console.WriteLine("Error: The Books table is empty and cannot add book to store balance");
                return;
            }

            if (storesList.Count < 1)
            {
                Console.WriteLine("Error: The Stores table is empty and cannot add store to store balance");
                return;
            }

            do
            {
                number = 1;
                Console.WriteLine("\nSelect a book from the list to add to a store:");
                foreach (Book book in booksList)
                {
                    Console.WriteLine(String.Format("{0,3}", number++.ToString()) + ": \"" + book.Title + "\" (" + book.Isbn13 + ")");
                }
                Console.Write("  > ");

                try
                {
                    input = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception)
                {
                    input = -1;
                    Console.WriteLine("Error: Invalid option entered, please try again...");
                }

            } while (input < 1 || input > booksList.Count);

            Book bookToAdd = booksList[input - 1];

            do
            {
                number = 1;
                Console.WriteLine("\nSelect a store from the list:");
                foreach (Store store in storesList)
                {
                    Console.WriteLine(String.Format("{0,3}", number++.ToString()) + ": \"" + store.StoreName + "\" (" + store.StoreAddress + ")");
                }
                Console.Write("  > ");

                try
                {
                    input = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception)
                {
                    input = -1;
                    Console.WriteLine("Error: Invalid option entered, please try again...");
                }

            } while (input < 1 || input > storesList.Count);

            Store storeToAdd = storesList[input - 1];

            bool updatedExistingEntry = false;

            foreach (StockBalance stockBalance in GetStockBalances(db))
            {
                if (stockBalance.BoutiqueId == storeToAdd.Id)
                {
                    if (stockBalance.Isbn == bookToAdd.Isbn13)
                    {
                        stockBalance.Number += 1;
                        db.SaveChanges();
                        Console.WriteLine("Updated inventory count for " + storeToAdd.StoreName + ": \"" + bookToAdd.Title + "\" (" + stockBalance.Number + ")");
                        updatedExistingEntry = true;
                    }
                }
            }

            if (updatedExistingEntry == false)
            {
                StockBalance newStockBalance = new StockBalance();
                newStockBalance.BoutiqueId = storeToAdd.Id;
                newStockBalance.Isbn = bookToAdd.Isbn13;
                newStockBalance.Number = 1;
                db.StockBalances.Add(newStockBalance);
                db.SaveChanges();
                Console.WriteLine("Updated inventory count for " + storeToAdd.StoreName + ": \"" + bookToAdd.Title + "\" (" + newStockBalance.Number + ")");
            }
        }

        static void RemoveBookFromStoreBalance(TBMContext db)
        {
            List<Store> storesList = GetListOfStores(db);
            int number;
            int input;

            do
            {
                number = 1;
                Console.WriteLine("\nSelect a store from the list:");
                foreach (Store store in storesList)
                {
                    Console.WriteLine(String.Format("{0,3}", number++.ToString()) + ": \"" + store.StoreName + "\" (" + store.StoreAddress + ")");
                }
                Console.Write("  > ");

                try
                {
                    input = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception)
                {
                    input = -1;
                    Console.WriteLine("Error: Invalid option entered, please try again...");
                }

            } while (input < 1 || input > storesList.Count);

            Store storeToRemove = storesList[input - 1];

            List<Book> booksList = GetListOfBooksForStoreToRemove(db, storeToRemove.Id);

            if (booksList.Count < 1)
            {
                Console.WriteLine("Error: The StockBalance table is empty for " + storeToRemove.StoreName + ", cannot remove book from store balance");
                return;
            }

            do
            {
                number = 1;
                Console.WriteLine("\nSelect a book from the list to remove from a store:");
                foreach (Book book in booksList)
                {
                    Console.WriteLine(String.Format("{0,3}", number++.ToString()) + ": \"" + book.Title + "\" (" + book.Isbn13 + ")");
                }
                Console.Write("  > ");

                try
                {
                    input = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception)
                {
                    input = -1;
                    Console.WriteLine("Error: Invalid option entered, please try again...");
                }

            } while (input < 1 || input > booksList.Count);

            Book bookToRemove = booksList[input - 1];

            foreach (StockBalance stockBalance in GetStockBalances(db))
            {
                if (stockBalance.BoutiqueId == storeToRemove.Id)
                {
                    if (stockBalance.Isbn == bookToRemove.Isbn13)
                    {
                        stockBalance.Number -= 1;
                        db.SaveChanges();
                        Console.WriteLine("Updated inventory count for " + storeToRemove.StoreName + ": \"" + bookToRemove.Title + "\" (" + stockBalance.Number + ")");
                    }
                }
            }

        }

        static void AddNewBookToDatabase(TBMContext db)
        {
            var BookInput = new string[6];
            var BookAuthorInput = new string[3];
            string[] PrintMessages = new string[] { "Please add ISBN: \"", "Please add Title: \"", "Please add Language: \"", "Please add Price: \"", "Please add Release Date: \"", "Please add Publisher: \"" };
            string[] PrintAuthorMessage = new string[] { "Please add First Name: \"", "Please add Last Name: \"", "Please add Date of Birth: \"" };

            for (int i = 0; i < PrintMessages.Length; i++)
            {
                Console.WriteLine(PrintMessages[i]);

                BookInput[i] = Console.ReadLine();
            }

            for (int i = 0; i < PrintAuthorMessage.Length; i++)
            {
                Console.WriteLine(PrintAuthorMessage[i]);

                BookAuthorInput[i] = Console.ReadLine();
            }

            var newBook = new Book()
            {
                Isbn13 = long.Parse(BookInput[0]),
                Title = BookInput[1]
            };

            var newAuthor = new Author()
            {
                FirstName = BookAuthorInput[0],
                LastName = BookAuthorInput[1],
                Dob = DateTime.Parse(BookAuthorInput[1])
            };


            db.Books.Add(newBook);
            db.Authors.Add(newAuthor);
            db.SaveChanges();
            Console.WriteLine("Added new book in Books database successfully!");

        }
        static void AddNewAuthorToDatabase(TBMContext db)
        {
            var BookAuthorInput = new string[3];
            string[] PrintAuthorMessage = new string[] { "Please add First Name: \"", "Please add Last Name: \"", "Please add Date of Birth: \"" };

            for (int i = 0; i < PrintAuthorMessage.Length; i++)
            {
                Console.WriteLine(PrintAuthorMessage[i]);

                BookAuthorInput[i] = Console.ReadLine();
            }

            var newAuthor = new Author()
            {
                FirstName = BookAuthorInput[0],
                LastName = BookAuthorInput[1],
                Dob = DateTime.Parse(BookAuthorInput[1])
            };

            db.Authors.Add(newAuthor);
            db.SaveChanges();
            Console.WriteLine("Added new author in Authors database successfully!");
        }

        static void EditBookDetailsInDatabase(TBMContext db)
        {

        }

        static void EditAuthorDetailsInDatabase(TBMContext db)
        {

        }

        static void DeleteBookFromDatabase(TBMContext db)
        {
            List<Book> booksList = GetListOfBooks(db);
            int number;
            int input;

            do
            {
                number = 1;
                Console.WriteLine("\nSelect a book from the list to remove:");
                foreach (Book book in GetListOfBooks(db))
                {
                    Console.WriteLine(String.Format("{0,3}", number++.ToString()) + ": \"" + book.Title + "\" (" + book.Isbn13 + ")");
                }
                Console.Write("  > ");

                try
                {
                    input = int.Parse(Console.ReadLine().ToString());
                }
                catch (Exception)
                {
                    input = -1;
                    Console.WriteLine("Error: Invalid option entered, please try again...");
                }

            } while (input < 1 || input > booksList.Count);

            Book bookToRemove = booksList[input - 1];

            foreach (StockBalance stockBalance in GetStockBalances(db))
            {
                if (stockBalance.Isbn == bookToRemove.Isbn13)
                {
                    db.Remove(bookToRemove);
                    db.SaveChanges();
                    Console.WriteLine("Successfully deleted" + bookToRemove.Title + "\" (" + bookToRemove.Isbn13 + ")");
                }
            }
        }

        static void DeleteAuthorFromDatabase(TBMContext db);
        {

        }

        static List<Book> GetListOfBooks(TBMContext db)
        {
            List<Book> list = new List<Book>();

            foreach (var item in db.Books.ToList())
            {
                list.Add(item);
            }

            return list;
        }

        static List<Book> GetListOfBooksForStoreToRemove(TBMContext db, int id)
        {
            List<Book> list = new List<Book>();

            foreach (var item in db.StockBalances.ToList())
            {
                if (item.BoutiqueId == id)
                {
                    foreach (var tempBook in db.Books)
                    {
                        if (tempBook.Isbn13 == item.Isbn)
                        {
                            if (item.Number > 0)
                            {
                                list.Add(tempBook);
                            }
                            break;
                        }
                    }
                }
            }

            return list;
        }
        static List<Store> GetListOfStores(TBMContext db)
        {
            List<Store> list = new List<Store>();

            foreach (var item in db.Stores.ToList())
            {
                list.Add(item);
                }

                return list;
            }

        static List<StockBalance> GetStockBalances(TBMContext db)
        {
            List<StockBalance> list = new List<StockBalance>();

            foreach (var item in db.StockBalances.ToList())
            {
                list.Add(item);
            }
            return list;
        }
    }
   




