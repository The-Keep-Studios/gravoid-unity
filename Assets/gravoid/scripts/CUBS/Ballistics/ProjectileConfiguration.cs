using System;
using System.Collections.Generic;

namespace TheKeepStudios.Gravoid.CUBS.Ballistics{

	public class ProjectileConfiguration : /*UnityEngine.ScriptableObject,*/ IProjectileConfiguration{


		private List<PartSelectionBehavior> parts = new List<PartSelectionBehavior>();


		public ProjectileConfiguration(){
		}

		#region IProjectileConfiguration implementation

		public void Copy(IProjectileConfiguration configToCopy){
			this.Parts = configToCopy.Parts;
		}

		public void Add(PartSelectionBehavior part){
			parts.Add(part);
		}

		public PartSelectionBehavior Head{
			get{
				return parts.Count > 0 ? parts[0] : null;
			}
			set{
				parts[0] = value;
			}
		}

		public List<PartSelectionBehavior> Body{
			get{
				//return the subset of parts from after the head to just before the tail
				return parts.Count > 2 ? parts.GetRange(1, parts.Count - 2) : new List<PartSelectionBehavior>();
			}
			set{
				/*
				 * It's simpliest to reconstruct the list, so save the current
				 * head and tail, clear the list and then remake it
				 */
				PartSelectionBehavior head = Head;
				PartSelectionBehavior tail = Tail;
				parts.Clear();
				parts.Add(head);
				parts.AddRange(value);
				parts.Add(tail);
			}
		}

		public PartSelectionBehavior Tail{
			get{
				return parts.Count > 0
					? parts[parts.Count - 1]
					: null;
			}
			set{
				if(parts.Count > 0){
					parts[parts.Count - 1] = value;
				} else{
					parts.Add(value);
				}
			}
		}

		public List<PartSelectionBehavior> Parts{
			get{
				return parts;
			}
			set{
				parts.Clear();
			}
		}

		public int Count{
			get{
				return parts.Count;
			}
		}

		public string Cost{
			get{
				throw new NotImplementedException();
			}
		}

		public string ReloadTime{
			get{
				throw new NotImplementedException();
			}
		}

		#endregion
	}
}

