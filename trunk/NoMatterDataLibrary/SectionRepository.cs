using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoMatterDataLibrary.Extensions;
using NoMatterDatabaseModel;
using Section = NoMatterWebApiModels.Models.Section;

namespace NoMatterDataLibrary
{
	public interface ISectionRepository
	{
		Task<int> AddSectionAsync(Section section);
		Task UpdateSectionAsync(Section section);
		Task<Section> GetSectionAsync(int sectionId);
		Task<List<Section>> GetClientSectionsAsync(Guid clientUuid, bool includeHidden);
		Task<List<Section>> GetClientSectionsAndCategoriesAsync(Guid clientUuid, bool includeHidden);
		Task DeleteSectionAsync(int sectionId);
	}

	public class SectionRepository : ISectionRepository
	{

		public async Task<int> AddSectionAsync(Section section)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var clientDb = await mainDb.Clients.Where(x => x.ClientUUID == new Guid(section.Client.ClientUuid)).FirstOrDefaultAsync();
				
				var sectionDb = section.ToDatabaseSection(clientDb.ClientId);

				sectionDb.SectionUUID = Guid.NewGuid();
				mainDb.Sections.Add(sectionDb);
				await mainDb.SaveChangesAsync();

				return sectionDb.SectionId;
			}
			
		}

		public async Task UpdateSectionAsync(Section section)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var sectionDb = await mainDb.Sections.Where(x => x.SectionId == section.SectionId).FirstOrDefaultAsync();

				sectionDb.SectionName = section.SectionName;
				sectionDb.SectionDescription = section.SectionDescription;
				sectionDb.SectionOrder = section.SectionOrder;
				sectionDb.Hidden = section.Hidden;
				sectionDb.Picture = section.Picture;

				await mainDb.SaveChangesAsync();
			}
		}

		public async Task<Section> GetSectionAsync(int sectionId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var section = await mainDb.Sections.Include("Client").Where(x => x.SectionId == sectionId).SingleOrDefaultAsync();
				return section.ToDomainSection();
			}
			
		}


		public async Task DeleteSectionAsync(int sectionId)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var section = await mainDb.Sections.Where(x => x.SectionId == sectionId).SingleOrDefaultAsync();

				if (section == null) return;

				mainDb.Sections.Remove(section);
				await mainDb.SaveChangesAsync();
			}
		}

		public async Task<List<Section>> GetClientSectionsAsync(Guid clientUuid, bool includeHidden)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var sections = mainDb.Sections.Include("Categories").Where(x => x.Client.ClientUUID == clientUuid);

				if (!includeHidden)
				{
					sections = sections.Where(x => !x.Hidden);
				}

				var sectionsDb = await sections.OrderBy(x => x.SectionOrder).ToListAsync();

				return sectionsDb.Select(x=>x.ToDomainSection()).ToList();
			}
			
		}

		public async Task<List<Section>> GetClientSectionsAndCategoriesAsync(Guid clientUuid, bool includeHidden)
		{
			using (var mainDb = new DatabaseEntities())
			{
				var sections = mainDb.Sections.Include("Categories").Include("Categories.Products").Where(x => x.Client.ClientUUID == clientUuid);

				if (!includeHidden)
				{
					sections = sections.Where(x => !x.Hidden);


					//sections = sections.SelectMany(x => x.Categories).Where(x => !x.Hidden);
					//sections = (sections.Where(s => s.Categories != null && (!s.Hidden && s.Categories.Where(y => !y.Hidden)))).

				}

				var sectionsDb = await sections.OrderBy(x => x.SectionOrder).ToListAsync();

				return sectionsDb.Select(x => x.ToDomainSection()).ToList();
			}
			
		}

		
	}
}
