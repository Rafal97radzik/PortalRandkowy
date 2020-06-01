using PortalRandkowy.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalRandkowy.API.DTOs
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }          
        public int Age { get; set; }                
        public string ZodiacSign { get; set; }      
        public DateTime Created { get; set; }       
        public DateTime LastActive { get; set; }    
        public string City { get; set; }            
        public string Country { get; set; }         
        public string Growth { get; set; }          
        public string EyeColor { get; set; }        
        public string HairColor { get; set; }       
        public string MartialStatus { get; set; }   
        public string Education { get; set; }       
        public string Profession { get; set; }      
        public string Children { get; set; }        
        public string Languages { get; set; }       
        public string Motto { get; set; }           
        public string Description { get; set; }     
        public string Personality { get; set; }     
        public string LookingFor { get; set; }      
        public string Interests { get; set; }       
        public string FreeTime { get; set; }        
        public string Sport { get; set; }           
        public string Movies { get; set; }          
        public string Music { get; set; }           
        public string ILike { get; set; }           
        public string IdoNotLike { get; set; }      
        public string MakesMeLaugh { get; set; }    
        public string ItFeelsBestIn { get; set; }   
        public string FriendeWouldDescribeMe { get; set; }  

        public ICollection<PhotosForDetailedDto> Photos { get; set; }
        public string PhotoUrl { get; set; }
    }
}
