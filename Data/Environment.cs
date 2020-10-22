    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    namespace blazorserver01.Data{
        public class Environment{
        private int rows = 1;
        private int cols = 1;
        private BioUnit[,] cell;
        public  Environment(int rows_, int colums_){
            this.rows = rows_;
            this.cols = colums_;
            this.cell = new BioUnit[this.rows,this.cols];
            for(var i=0; i<this.rows; i++)
            for(var j=0; j<this.cols; j++)
            this.cell[i,j] = null;
        }
        public int total_of_rows(){
            return this.rows;
        }public int total_of_cols(){
            return this.cols;
        }        
        private bool rightPos(int i, int j){
            return ((i>=0) && (i<this.rows) && (j>=0) && (j<this.cols));
        }
        public void insert(int i,int j,BioUnit been){
            if(this.rightPos(i,j)){
                this.cell[i,j] = been;
            }
        }
        public BioUnit biounit(int i,int j){
            if(this.rightPos(i,j)){
                return this.cell[i,j];
            }
            return null;
        }
        public List<BioUnit> neighbors(int i,int j){
            List<BioUnit> ans = new List<BioUnit>();
            if(this.rightPos(i,j)){
                if(this.rightPos(i-1,j-1) && this.cell[i-1,j-1]!=null) ans.Add(this.cell[i-1,j-1]);
                if(this.rightPos(i-1,j) && this.cell[i-1,j]!=null) ans.Add(this.cell[i-1,j]);
                if(this.rightPos(i-1,j+1) && this.cell[i-1,j+1]!=null) ans.Add(this.cell[i-1,j+1]);
                if(this.rightPos(i,j-1) && this.cell[i,j-1]!=null) ans.Add(this.cell[i,j-1]);
                if(this.rightPos(i,j+1) && this.cell[i,j+1]!=null) ans.Add(this.cell[i,j+1]);
                if(this.rightPos(i+1,j-1) && this.cell[i+1,j-1]!=null) ans.Add(this.cell[i+1,j-1]);
                if(this.rightPos(i+1,j) && this.cell[i+1,j]!=null) ans.Add(this.cell[i+1,j]);
                if(this.rightPos(i+1,j+1) && this.cell[i+1,j+1]!=null) ans.Add(this.cell[i+1,j+1]);                
            }
        return ans;
        }
        public int surroundingNeighbors(int i,int j,String specie){
            int ans=0;
            List<BioUnit> surr = this.neighbors(i,j);
            Console.WriteLine("  i   j  "+i.ToString()+" , "+j.ToString());
            foreach (object unit in surr){
                if(this.specie(unit) == specie) ans++;
            }
            return ans;
        }
        public String specie(Object obj){
            String[] w;
            if(obj==null) return "";
            w = TypeDescriptor.GetClassName(obj).Split(".");
            return w[w.Length-1];
        }
    public Rabbit firstRabbit(int i,int j){
        List<BioUnit> neis = this.neighbors(i,j);
        foreach(object unit in neis){
            if(this.specie(unit)=="Rabbit")
                return (Rabbit) unit;
        }
        return null;
    }
    public void next_Rabbit_Carrot_Step(){
        BioUnit[,]  aux = new BioUnit[this.rows,this.cols];
        for(var i=0; i<this.rows; i++)
        for(var j=0; j<this.cols; j++){
          aux[i,j] = null;
          if(this.specie(this.cell[i,j]) == "Carrot"){
              if(this.surroundingNeighbors(i,j,"Rabbit") ==0){
                  if(this.cell[i,j].will_I_live())
                      aux[i,j] = this.cell[i,j];
              }
              else {
                  this.firstRabbit(i,j).eat();
              }
          }
          else if (this.specie(this.cell[i,j])=="Rabbit"){
              if(this.cell[i,j].will_I_live()){
                      aux[i,j] = this.cell[i,j];
          }
          }
          else {
              if(this.cell[i,j]==null){
                  if(this.surroundingNeighbors(i,j,"Rabbit")>=2)
                      aux[i,j] = new Rabbit(i,j,this);
                  else if  (this.surroundingNeighbors(i,j,"Carrot")>=3)
                      aux[i,j] = new Carrot(i,j,this);
           }
        }
        }
    for(var i=0; i<this.rows; i++)
    for(var j=0; j<this.cols; j++){
        this.cell[i,j] = aux[i,j];
       }
    } 
  }
}