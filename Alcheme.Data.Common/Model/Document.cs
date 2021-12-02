using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Alcheme.Data.Common.Model
{
    public class Document : BSON
    {

        private string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        private string createdBy { get; set; }
        public string CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        private DateTime receivedDate { get; set; }
        public DateTime ReceivedDate
        {
            get
            {
                return receivedDate;
            }
            set
            {
                receivedDate = value;
            }
        }

        private DateTime dueDate { get; set; }
        public DateTime DueDate
        {
            get
            {
                return dueDate;
            }
            set
            {
                dueDate = value;
            }
        }

        private int quantity { get; set; }
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        private List<Approver> approvals { get; set; }
        public List<Approver> Approvals
        {
            get
            {
                return approvals;
            }
            set
            {
                approvals = value;
            }
        }

        private string status { get; set; }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        private double size { get; set; }
        public double Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
        private DateTime lastUpdated { get; set; }
        public DateTime LastUpdated
        {
            get
            {
                return lastUpdated;
            }
            set
            {
                lastUpdated = value;
            }
        }

    }
}
