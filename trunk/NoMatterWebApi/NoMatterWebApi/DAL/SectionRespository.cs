using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.DAL
{

	public interface ISectionRespository
	{
		Task<Section> GetSectionAsync(Guid sectionUuid);
		Task<List<Category>> GetSectionCategoriesAsync(Guid sectionUuid);
	}

	public class SectionRespository : ISectionRespository
	{
		private DatabaseEntities databaseConnection;

		public SectionRespository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<Section> GetSectionAsync(Guid sectionUuid)
		{
			var section = await databaseConnection.Sections.Where(x => x.SectionUUID == sectionUuid).SingleOrDefaultAsync();
			return section;
		}

		public async Task<List<Category>> GetSectionCategoriesAsync(Guid sectionUuid)
		{
			var categories = await databaseConnection.Categories.Include("Section").Where(x => x.Section.SectionUUID == sectionUuid).ToListAsync();
			return categories;
		}

		public void Save()
		{
			databaseConnection.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					databaseConnection.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}

}
