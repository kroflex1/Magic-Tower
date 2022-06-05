using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MagicTower.Model.Items;
using MagicTower.Model.MagicModels;

namespace MagicTower.Model
{
    public class TreasureRoom : Room
    {
        public bool IsEmpty
        {
            get => ItemsInRoom.Count == 0;
        }

        private List<Type> typesOfArtifacts;
        private List<Type> typesOfMagicForScrolls;
        private List<Type> bannedMagic;

        public TreasureRoom(int width, int height, Player player) : base(width, height, player)
        {
            SetAvailableTypesOfArtifacts();
            SetBannedMagic();
            SetAvailableTypesOfMagicForScrolls();
           
        }

        public void UpdateTreasures()
        {
            SpawnRandomArtifact();
            SpawnRandomScroll();
        }

        private void SpawnRandomArtifact()
        {
            var artifactType = typesOfArtifacts[new Random().Next(0, typesOfArtifacts.Count)];
            SpawnItem((Item) Activator.CreateInstance(artifactType, width / 2, height / 2));
        }

        private void SpawnRandomScroll()
        {
            var magicForScroll = typesOfMagicForScrolls[new Random().Next(0, typesOfMagicForScrolls.Count)];
            var scroll = new Scroll(width / 2 + 64, height / 2, magicForScroll);
            SpawnItem(scroll);
        }

        private void SetAvailableTypesOfArtifacts()
        {
            typesOfArtifacts = new List<Type>()
            {
                typeof(DragonsEye),
                typeof(MagicMushroom),
                typeof(EldenRing)
            };
        }

        private void SetAvailableTypesOfMagicForScrolls()
        {
            var magicForScroll = typeof(MagicModels.Magic);
            typesOfMagicForScrolls = Assembly.GetAssembly(magicForScroll).GetTypes()
                .Where(type => type.IsSubclassOf(magicForScroll) && !bannedMagic.Contains(type))
                .ToList();
        }

        private void SetBannedMagic()
        {
            bannedMagic = new List<Type>()
            {
                typeof(IceShard)
            };
        }
    }
}