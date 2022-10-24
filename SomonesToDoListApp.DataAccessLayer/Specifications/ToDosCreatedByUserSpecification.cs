using System;
using System.Linq.Expressions;
using SomeonesToDoListApp.DataAccessLayer.Entities;

namespace SomeonesToDoListApp.DataAccessLayer.Specifications
{
    public class ToDosCreatedByUserSpecification : Specification<ToDo>
    {
        private readonly string _userId;

        public ToDosCreatedByUserSpecification(string userId)
        {
            _userId = userId;
        }

        public override Expression<Func<ToDo, bool>> ToExpression()
        {
            return toDo => toDo.CreatedBy == _userId;
        }
    }
}