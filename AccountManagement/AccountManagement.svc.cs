using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AccountManagement
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountManagement" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AccountManagement.svc or AccountManagement.svc.cs at the Solution Explorer and start debugging.
    public class AccountManagement : IAccountManagement
    {
        public string BASE_URL_EMPMAN = "http://localhost:48757/EmployeeManagement.svc/";
        public string BASE_URL_ASSIGN = "http://localhost:17636/AccountAssignment.svc/";
        public string BASE_URL_ACCMAN = "http://localhost:49443/AccountManagement.svc/";

        // PRONAĐI SVE RAČUNE
        public List<Account> findAllAcc()
        {
            using(BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                return ben.AccountTables.Select(at => new Account
                    {
                        Id = at.AccID,
                        Ime = at.ImeAcc,
                        Tip = at.TipAcc,
                        Kreirano = at.Kreirano,
                        Izmjena = at.Izmjena
                    }).ToList();
            }
        }

       

        public List<ManyToManyFindAccRole> findAllAccByRol(string id)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                int cid = Convert.ToInt32(id);
                var x = from b in ben.AccountTables
                        join a in ben.AccRoleMappTables on b.AccID equals a.AccID
                        where a.RoleID == cid
                        select new ManyToManyFindAccRole { AccID = b.AccID, ImeAccounta = b.ImeAcc, TipAccounta = b.TipAcc, RoleID = a.RoleID };
                return x.ToList();


                /*var ispis =  ben.AccRoleMappTables.Where(acm => acm.AccID == cid).Select(ac => new AccRoleMapp
                {
                    RoleId = ac.RoleID
                }).ToList();

                return ispis;*/





                /* var ispis = from a in ben.AccountTables
                             from b in ben.AccRoleMappTables
                             where a.AccID == cid
                             select a;
                 var drugi = from c in ben.RoleTables
                             from d in ben.AccRoleMappTables
                             where c.RoleID == cid
                             select c;
                 return ispis;*/



                //var x = from a in ben.AccountTables from b in ben.RoleTables where b.RoleID == cid select b;
                //return x.ToList();



                /*
                var x = from b in ben.AccountTables
                        join a in ben.AccRoleMappTables on b.AccID equals a.AccID

                       from c in ben.RoleTables// ovo je višak
                        join d in ben.AccRoleMappTables on c.RoleID equals d.RoleID // ovo je višak 
                         select new ManyToManyIspis{ ImeAccounta = b.ImeAcc, TipAccounta = b.TipAcc, RoleID = a.RoleID };
                return x.ToList();*/


                //return ben.AccountTables.Where(s => s.AccRoleMappTables.Any(e => e.RoleTable.RoleID == cid));


                /*return ben.AccRoleMappTables.Where(at => at.RoleID == cid).Select(ata => new AccRoleMapp
                {
                    AccId = ata.AccID
                }).ToList();*/
            }

        }


        // PRONAĐI SVE ROLE VEZTANE ZA POJEDINI RAČUN
        public List<ManyToManyFindRolAcc> findAllRolByAcc(string id)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                int cid = Convert.ToInt32(id);
                var x = from c in ben.RoleTables
                        join d in ben.AccRoleMappTables on c.RoleID equals d.RoleID
                        where d.AccID == cid
                        select new ManyToManyFindRolAcc { ImeRole = c.ImeRole, RoleID = c.RoleID };
                return x.ToList();
            }

        }


        // PRONAĐI SPECIFIČAN RAČUN (PREMA ID-ju)
        public Account findAcc(string id)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                int cid = Convert.ToInt32(id);
                return ben.AccountTables.Where(at => at.AccID == cid).Select(ata => new Account
                {
                    Id = ata.AccID,
                    Ime = ata.ImeAcc,
                    Tip = ata.TipAcc,
                    Kreirano = ata.Kreirano,
                    Izmjena = ata.Izmjena
                }).First();
            }
        }


        // KREIRAJ NOVI RAČUN
        public bool createAcc(Account account)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                try
                {
                    AccountTable at = new AccountTable();
                    
                    at.ImeAcc = account.Ime;
                    at.TipAcc = account.Tip;
                    at.Kreirano = account.Kreirano;
                    at.Izmjena = account.Izmjena;
                    ben.AccountTables.Add(at);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }





        /*

    // KREIRAJ NOVI RAČUN (accandaccrolezajedno klasa)
    public bool createAcc(AccAndAccRoleZajedno accandaccrolezajedno)
    {
        using (BazaAccountManEntities ben = new BazaAccountManEntities())
        {
            try
            {
                AccountTable at = new AccountTable();
                AccRoleMappTable acmt = new AccRoleMappTable();
                //int id = Convert.ToInt32(account.Id); // spremanje id-ja za korištenje kod verifikacije(kad kreiraš novi axxount,treba mu dodati role.Role dodaješ tako da ovaj id ubaciš u funkciju za dodavanje rola)

                at.ImeAcc = accandaccrolezajedno.Ime;
                at.TipAcc = accandaccrolezajedno.Tip;
                at.Kreirano = accandaccrolezajedno.Kreirano;
                at.Izmjena = accandaccrolezajedno.Izmjena;
                acmt.AccID = accandaccrolezajedno.AccId;
                acmt.RoleID = accandaccrolezajedno.RoleId;
                ben.AccountTables.Add(at);
                ben.AccRoleMappTables.Add(acmt);
                ben.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }*/






        // OBRIŠI JEDNOG ACCOUNT
        public bool deleteAcc(Account account)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                try
                {
                    int cid = Convert.ToInt32(account.Id);
                    AccountTable at = ben.AccountTables.Single(ata => ata.AccID == cid);
                    ben.AccountTables.Remove(at);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        // UPDATE ACCOUNT
        public bool updateAccData(Account account)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                try
                {
                    int cid = Convert.ToInt32(account.Id);
                    AccountTable at = ben.AccountTables.Single(ata => ata.AccID == cid);
                    at.ImeAcc = account.Ime;
                    at.TipAcc = account.Tip;
                    at.Kreirano = account.Kreirano;
                    at.Izmjena = account.Izmjena;
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }



        // ADD ROLE TO ACC 
        public bool addRoleToAcc(AccRoleMapp accrolemapp)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                try
                {
                    AccRoleMappTable armt = new AccRoleMappTable();
                    armt.RoleID = accrolemapp.RoleId;
                    ben.AccRoleMappTables.Add(armt);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }


        // PRONAĐI ACC U ACCROLEMAPP (jedna vrijednost)
        public AccRoleMapp findAccRoleMapp(string id)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                int cid = Convert.ToInt32(id);
                return ben.AccRoleMappTables.Where(acm => acm.AccID == cid).Select(acmt => new AccRoleMapp
                {
                    AccId = acmt.AccID,
                    //RoleId = acmt.RoleID
                }).First();
            }
        }


        // REMOVE ROLE FROM ACC 
        public bool removeRoleFromAcc(AccRoleMapp accrolemapp)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                try
                {
                    int cid = Convert.ToInt32(accrolemapp.AccId);
                    AccRoleMappTable armt = ben.AccRoleMappTables.Single(arm => arm.AccID == cid);
                    ben.AccRoleMappTables.Remove(armt);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }



        // PRONAĐI SVE RAČUNE VEZANE ZA POJEDINU ROLU
        public List<ManyToManyFindAccRole> findAccsForRole(string id)
        {
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                int cid = Convert.ToInt32(id);
                var x = from b in ben.AccountTables
                        join a in ben.AccRoleMappTables on b.AccID equals a.AccID
                        where a.RoleID == cid
                        select new ManyToManyFindAccRole { AccID = b.AccID, ImeAccounta = b.ImeAcc, TipAccounta = b.TipAcc, RoleID = a.RoleID };
                return x.ToList();
            }

        }



        // ADD ACCOUNT TO ROLE (CREATE)
        public bool addAccToRole(AccRoleMapp accrolemapp)
        {
            int idRole = Convert.ToInt32(accrolemapp.RoleId);
            int idAccounta = Convert.ToInt32(accrolemapp.AccId);
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                try
                {
                    AccRoleMappTable armt = new AccRoleMappTable();
                    armt.RoleID = idRole;
                    armt.AccID = idAccounta;
                    ben.AccRoleMappTables.Add(armt);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /*
        // REMOVE ROLE FROM ACC (DELETE)
        public bool removeAccFromRole(AccRoleMapp accrolemapp)
        {
   
            int idAccounta = Convert.ToInt32(accrolemapp.AccId);
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                try
                {
                    AccRoleMappTable armt = ben.AccRoleMappTables.Single(arm => arm.AccID == idAccounta);
                    ben.AccRoleMappTables.Remove(armt);
                    ben.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }*/

        // REMOVE ROLE FROM ACC (DELETE)
        public bool removeAccFromRole(AccRoleMapp accrolemapp)
        {
            /*
            int idAccounta = Convert.ToInt32(accrolemapp.AccId);
            using (BazaAccountManEntities ben = new BazaAccountManEntities())
            {
                try
                {
                    List<EmpAccMappTable> eamList = new List<EmpAccMappTable>();
                    int cid = Convert.ToInt32(empaccmapp.ZapId);
                    foreach (var obj in listaIspis)
                    {

                        EmpAccMappTable eamt = new EmpAccMappTable();

                        ben.EmpAccMappTables.RemoveRange(ben.EmpAccMappTables.Where(eam => eam.ZaposlenikID == cid));
                        ben.SaveChanges();

                    }
                    ben.SaveChanges();*/
                    return true;
                //}
                //catch
                //{
                    //return false;
        
            }
    }
}

