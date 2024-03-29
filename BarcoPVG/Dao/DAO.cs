﻿namespace BarcoPVG.Dao
{
    //Jarne moving the specific dao methods to the right Dao class
    //all general methods are being kept inside this class

    // SINGLETON PATTERN
    // Private constructor, static instance
    // Ensures only one DBconnection is opened at a time
    // Ensures connection is closed when not in use
    public class DAO
    {
        // Variables
        public BarcoContext _context;
        protected static readonly DAO _instance = new();

        // Calls an DAO instance
        public static DAO Instance()
        {
            return _instance;
        }

        // DAO Constructor - PRIVATE
        // Calls an instance from the Barco2021Context and stores this context in the current context
        protected DAO()
        {
            this._context = new BarcoContext();
        }

        // Removes unsaved changed by replacing the context by a new instance
        // Kaat
        public void RemoveChanges()
        {
            _context = new BarcoContext();
        }

        // SAVING
        // Stores all data from GUI in DB
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}