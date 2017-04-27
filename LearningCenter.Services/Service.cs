namespace LearningCenter.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using LearningCenter.Data;
    using LearningCenter.Models.EntityModels;
    using LearningCenter.Models.ViewModels.Admin;

    public abstract class Service
    {
        public Service()
        {
            this.Context = new LearningCenterContext();
        }

        protected LearningCenterContext Context { get; set; }

        protected User GetCurrentUser(string userId)
        {
            return this.Context.Users.Find(userId);
        }

        public IEnumerable<UnitListViewModel> GetAllUnits()
        {
            IEnumerable<Unit> units = this.Context.Units;
            var viewModels = Mapper.Instance
                .Map<IEnumerable<Unit>, IEnumerable<UnitListViewModel>>(units).ToArray();

            for (int i = 0; i < viewModels.Length; i++)
            {
                var unit = units.FirstOrDefault(u => u.Id == viewModels[i].Id);
                if (unit.CourseId != null)
                {
                    viewModels[i].CourseName = unit.Course.Title;
                }
                else
                {
                    viewModels[i].CourseName = "Unassigned";
                }
            }

            return viewModels;
        }

        public IEnumerable<UnitListViewModel> SearchUnits(string search)
        {
            IEnumerable<UnitListViewModel> viewModels = this.GetAllUnits();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                viewModels = viewModels.Where(model =>
                    model.Title.ToLower().Contains(search)
                    || model.CourseName.ToLower().Contains(search));
            }

            return viewModels;
        }

    }
}
