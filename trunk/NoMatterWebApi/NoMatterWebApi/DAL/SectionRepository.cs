using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDatabaseModel;

namespace NoMatterWebApi.DAL
{

	public interface ISectionRepository
	{
		Task AddSectionAsync(Section section);
		Task UpdateSectionAsync(Section sectionDb, NoMatterWebApiModels.Models.Section section);
		Task<Section> GetSectionAsync(Guid sectionUuid);
		Task<List<Category>> GetSectionCategoriesAsync(Guid sectionUuid);
		Task DeleteSectionAsync(Guid sectionUuid);
	}

	public class SectionRepository : ISectionRepository
	{
		private DatabaseEntities databaseConnection;

		public SectionRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task AddSectionAsync(Section section)
		{
			section.SectionUUID = Guid.NewGuid();
			databaseConnection.Sections.Add(section);
			await databaseConnection.SaveChangesAsync();
		}

		public async Task UpdateSectionAsync(Section sectionDb, NoMatterWebApiModels.Models.Section section)
		{

			databaseConnection.Sections.Attach(sectionDb);

			sectionDb.SectionName = section.SectionName;
			sectionDb.SectionDescription = section.SectionDescription;
			sectionDb.SectionOrder = section.SectionOrder;
			sectionDb.Hidden = section.Hidden;
			sectionDb.Picture = section.Picture;

			await databaseConnection.SaveChangesAsync();

		}

		public async Task<Section> GetSectionAsync(Guid sectionUuid)
		{
			var section = await databaseConnection.Sections.Include("Client").Where(x => x.SectionUUID == sectionUuid).SingleOrDefaultAsync();
			return section;
		}

		public async Task<List<Category>> GetSectionCategoriesAsync(Guid sectionUuid)
		{
			var categories = await databaseConnection.Categories
				.Include("Section")
				.Include("Products")
				.Where(x => x.Section.SectionUUID == sectionUuid && !x.Hidden)
				.OrderBy(x=>x.CategoryOrder)
				.ToListAsync();

			return categories;
		}

		public async Task DeleteSectionAsync(Guid sectionUuid)
		{
			var section = await databaseConnection.Sections.Where(x => x.SectionUUID == sectionUuid).SingleOrDefaultAsync();

			databaseConnection.Sections.Remove(section);
			await databaseConnection.SaveChangesAsync();

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
