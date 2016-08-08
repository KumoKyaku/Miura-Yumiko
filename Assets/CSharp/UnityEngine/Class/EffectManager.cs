using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVG;
using UnityEngine;

namespace Poi
{
    public class EffectManager
    {
        public static void Play(IList<iEffect> effectList)
        {
            if (!GameManager.Save.Use5MaoEffect)
            {
                return;
            }
            LifeRun();
            foreach (var item in effectList)
            {
                ViewEffect eff = GetEffect();
                eff.Life = (int)item.Life;

                if (item.Owner == 100100)
                {
                    eff.owner = GameManager.AVGPlayer.SceneEff;
                    eff.SetPos(item.Pos);
                    eff.SetSize(item.Size);
                }
                else
                {
                    if (!GameManager.AVGPlayer.Charactor.ActorList.ContainsKey(item.Owner))
                    {
                        continue;
                    }
                    eff.owner = GameManager.AVGPlayer.Charactor.ActorList[item.Owner];
                    eff.SetPos(item.Pos);
                    eff.SetSize(item.Size*100);
                }

                AddLiveEffect(eff);
                eff.gameObject.SetActive(true);
                eff.Play(item.Name,true);        
            }
        }

        private static void LifeRun()
        {
            try
            {
                foreach (var item in efflist)
                {
                    if (item.Life > 0)
                    {
                        item.Life--;
                        if (item.Life == 0)
                        {
                            item.gameObject.SetActive(false);
                        }
                    }
                }
            }
            catch (Exception e)
            {
#if UNITY_EDITOR || Development
                Debuger.LogError(e);
#endif
            }
        }

        public static void Clear()
        {
            efflist.Clear();
        }

        private static void AddLiveEffect(ViewEffect eff)
        {
            if (efflist.Contains(eff))
            {

            }
            else
            {
                efflist.Add(eff);
            }
        }

        static List<ViewEffect> efflist = new List<ViewEffect>();
        private static ViewEffect GetEffect()
        {
            var teff = efflist.Find(eff => eff.Life == 0);
            if (teff == null)
            {
                var go = Loader.CreateObject(CFG.PreparePath + "SpriteEffect");
                teff = go.GetComponent<ViewEffect>();
            }
            return teff;
        }
    }
}
