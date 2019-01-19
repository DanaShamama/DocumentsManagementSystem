using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsManagementSystem.DataStructures
{
    public class BST<T> where T : IComparable<T>
    {
        //data members
        BSTNode root;
        IComparer<T> alternativeComparer;
        
        //c'tor
        public BST(IComparer<T> alternativeComparer = null)
        {
            this.alternativeComparer = alternativeComparer;
        }

        //functions
        private int ItemsCompare(T item1, T item2)
        {
            if (alternativeComparer == null)
                return item1.CompareTo(item2);// from IComparable - default
            return alternativeComparer.Compare(item1, item2);
        }

        public void Add(T newData)
        {
            BSTNode newNode = new BSTNode(newData);
            if (root == null)
            {
                root = newNode;
                return;
            }
            BSTNode tmp = root;
            BSTNode parent = null;

            while (tmp != null)
            {
                parent = tmp;

                if (ItemsCompare(newData, tmp.data) < 0)
                    tmp = tmp.left;
                else
                    tmp = tmp.right;
            }
            if (ItemsCompare(newData, parent.data) < 0)
                parent.left = newNode;
            else
                parent.right = newNode;
        }

        public bool Search(T item, out T foundItem)
        {
            BSTNode tmp = root;
            foundItem = default(T);
            while (tmp != null)
            {
                if (ItemsCompare(item, tmp.data) == 0)
                {
                    foundItem = tmp.data;
                    return true;
                }
                if (ItemsCompare(item, tmp.data) < 0)
                    tmp = tmp.left;
                else
                    tmp = tmp.right;
            }
            return false;
        }

        public bool SearchNext(T item, out T foundItem)
        {
            BSTNode tmp = root;
            foundItem = default(T);
            T keep = root.data;
            while (tmp != null)
            {
                if (ItemsCompare(item, tmp.data) == 0)
                {
                    foundItem = tmp.data;
                    return true;
                }
                if (ItemsCompare(item, tmp.data) < 0)
                    tmp = tmp.left;
                else
                    tmp = tmp.right;

                if (tmp != null && ItemsCompare(tmp.data, item) > 0 && ItemsCompare(tmp.data, keep) < 0)
                {
                    keep = tmp.data;
                }
            }

            if (ItemsCompare(keep, item) > 0)
            {
                foundItem = keep;
                return true;
            }

            return false;
        }

        public void Remove(T item, out T removedItem)
        {
            Remove(item, out removedItem, root, null);
        }

        private bool Remove(T item, out T removedItem, BSTNode current_root, BSTNode parentNode)
        {
            removedItem = default(T);

            if (current_root == null)
                return false;

            int res = ItemsCompare(item, current_root.data);

            if (res == 0)
            {
                removedItem = current_root.data;

                if (parentNode == null && current_root.left == null && current_root.right == null)
                {
                    root = null;
                    return true;
                }

                if (current_root.left == null && current_root.right == null)
                {

                    if (ItemsCompare(current_root.data, parentNode.data) < 0) // parent  from right
                    {
                        parentNode.left = null;
                        return true;
                    }

                    if (ItemsCompare(current_root.data, parentNode.data) >= 0) // parent  from left
                    {
                        parentNode.right = null;
                        return true;
                    }
                }

                if (current_root.left == null && current_root.right != null)
                {
                    removedItem = current_root.data;

                    if (parentNode != null && parentNode.left == current_root)
                    {
                        parentNode.left = current_root.right;
                        return true;
                    }
                    else if (parentNode != null)
                    {
                        parentNode.right = current_root.right;
                        return true;
                    }
                    
                }

                if (current_root.left != null && current_root.right == null)
                {
                    removedItem = current_root.data;

                    if (parentNode != null && parentNode.left == current_root)
                    {
                        parentNode.left = current_root.left;
                        return true;
                    }
                    else if (parentNode != null)
                    {
                        parentNode.right = current_root.left;
                        return true;
                    }
                    
                }

                if (current_root.left != null && current_root.right != null)
                {
                    removedItem = current_root.data;
                    T tmp = current_root.data;
                    current_root.data = current_root.right.data;
                    Remove(current_root.right.data, out tmp, current_root.right, current_root);
                }
            }

            if (res < 0)
            {
                Remove(item, out removedItem, current_root.left, current_root);
            }

            if (res > 0)
            {
                Remove(item, out removedItem, current_root.right, current_root);
            }

            return false;
        }

        class BSTNode
        {
            public T data;
            public BSTNode left;
            public BSTNode right;

            public BSTNode(T data)
            {
                this.data = data;
            }
        }
    }
}

