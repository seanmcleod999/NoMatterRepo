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
		Task<int> AddSectionAsync(Section section);
		Task UpdateSectionAsync(Section sectionDb, NoMatterWebApiModels.Models.Section section);
		Task<Section> GetSectionAsync(Guid sectionUuid);
		Task<List<Section>> GetClientSectionsAsync(Guid clientUuid, bool includeHidden);
		Task<List<Section>> GetClientSectionsAndCategoriesAsync(Guid clientUuid, bool includeHidden);	
		Task DeleteSectionAsync(Guid sectionUuid);


	}

	public class SectionRepository : ISectionRepository
	{
		private DatabaseEntities databaseConnection;

		public SectionRepository(DatabaseEntities databaseConnection)
		{
			this.databaseConnection = databaseConnection;
		}

		public async Task<int> AddSectionAsync(Section section)
		{
			section.SectionUUID = Guid.NewGuid();
			databaseConnection.Sections.Add(section);
			await databaseConnection.SaveChangesAsync();

			return section.SectionId;
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

		
		public async Task DeleteSectionAsync(Guid sectionUuid)
		{
			var section = await databaseConnection.Sections.Where(x => x.SectionUUID == sectionUuid).SingleOrDefaultAsync();

			if (section == null) return;

			databaseConnection.Sections.Remove(section);
			await databaseConnection.SaveChangesAsync();

		}

		public async Task<List<Section>> GetClientSectionsAsync(Guid clientUuid, bool includeHidden)
		{
			var sections = databaseConnection.Sections.Include("Categories").Where(x => x.Client.ClientUUID == clientUuid);

			if (!includeHidden)
			{
				sections = sections.Where(x => !x.Hidden);
			}

			var sectionsDb = await sections.OrderBy(x => x.SectionOrder).ToListAsync();

			return sectionsDb;
		}

		public async Task<List<Section>> GetClientSectionsAndCategoriesAsync(Guid clientUuid, bool includeHidden)
		{
			var sections = databaseConnection.Sections.Include("Categories").Include("Categories.Products").Where(x => x.Client.ClientUUID == clientUuid);

			if (!includeHidden)
			{
				sections = sections.Where(x => !x.Hidden);


				//sections = sections.SelectMany(x => x.Categories).Where(x => !x.Hidden);
				//sections = (sections.Where(s => s.Categories != null && (!s.Hidden && s.Categories.Where(y => !y.Hidden)))).

			}

			var sectionsDb = await sections.OrderBy(x => x.SectionOrder).ToListAsync();

			return sectionsDb;
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
