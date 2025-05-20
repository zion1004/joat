public class Stat
{
    /*
        list[x][0] = min jump force
        list[x][1] = max jump force
        list[x][2] = min spin force
        list[x][3] = max spin force
        list[x][4] = jump side force
        list[x][5] = double jump force
        list[x][6] = double jump spin force
        list[x][7] = double jump side force
        list[x][8] = jump cooldown
        list[x][9] = movement angular rotation force
        list[x][10] = max movement angular rotation force
        list[x][11] = max angular velocity
        list[x][12] = attack
        list[x][13] = durability

        list[x][14] = type => 0 = base, 1 = water, 2 = fire, 3 = poison
     */
    int[,] list = {
        { 15, 30, 25, 50, 8, 1, 30, 30, 1000, 1000, 10, 10, 100, 0 },  // katana
        { 20, 40, 30, 60, 10, 2, 50, 50, 1000, 1000, 10, 10, 100, 1 },  // aqua blade
        { 20, 40, 30, 60, 10, 2, 50, 50, 1000, 1000, 10, 10, 100, 2 },  // fire sword
        { 20, 40, 30, 60, 10, 2, 50, 50, 1000, 1000, 10, 10, 100, 3 }  // poison sabre
    };
}
