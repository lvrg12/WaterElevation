import csv

def isValid(obj):
    #print(obj)
    if obj is not None:
        if obj != '':
            return True
    return False

def main(county):
    x = 3.28084
    with open("well_data.csv") as wd:
        st = {}
        line = wd.readline()
        while line:
            line = wd.readline().strip("\n")
            arr = line.split(",")
            if(len(arr)>1 and arr[4] == county):
                st[arr[0]] = [arr[8],arr[9],arr[5],arr[6],arr[7]]
            line = wd.readline()

    with open("WellMain.txt") as wellmain:
        with open(county+"_optimized.csv", 'w', newline='') as f:
            writer = csv.writer(f)
            big_arr = []
            small_arr = ["id","county","latitude","longitude","wellDepth","LandSurfaceElevation","WaterElevation","Saturated Thickness","DepthFromLSD","Month","Day","Year"]
            big_arr.append(small_arr)
            line = wellmain.readline()
            line = wellmain.readline().strip("\n")
            done = []
            
            while line:
                
                arr = line.split("|")
                if(arr[1]==county and isValid(arr[12]) and isValid(arr[16]) and isValid(arr[23])):
                    
                    if(arr[0] in st and arr[0] not in done):
                        small_arr = [arr[0],arr[1],arr[12],arr[16],float(arr[23])/x,float(arr[25])/x,float(st[arr[0]][0])/x,st[arr[0]][1],str(float(float(arr[25])-float(st[arr[0]][0]))/x),st[arr[0]][2],st[arr[0]][3],st[arr[0]][4]]
                        done.append(arr[0])

                        big_arr.append(small_arr)
                line = wellmain.readline().strip("\n")

            writer.writerows(big_arr)

if __name__ == "__main__":
    main("Lubbock")